using FootballLeague.DAL;
using FootballLeague.Services.CalculationStrategy;
using FootballLeague.Services.Constants;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.DTOs.RequestDTOs;
using FootballLeague.Services.DTOs.ResponseDTOs;
using FootballLeague.Services.Exceptions;
using FootballLeague.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Match = FootballLeague.DAL.Models.Match;

namespace FootballLeague.Services.Services
{
    public class MatchService : IMatchService
    {
        private readonly FootballLeagueContext _context;
        private readonly ILeagueRankingService _leagueRankingService;
        private readonly ITeamService _teamService;

        public MatchService(FootballLeagueContext context, ILeagueRankingService resultService, ITeamService teamService)
        {
            _context = context;
            _leagueRankingService = resultService;
            _teamService = teamService;
        }

        public async Task<Match> CreateMatchAsync(MatchRequestDTO model)
        {
            MatchRequestDTOValidator(model);

            await _leagueRankingService.LeagueRankingExistsValidator(model.LeagueRankingId);

            await _teamService.TeamExistsValidator(model.HomeTeamId, model.LeagueRankingId);
            await _teamService.TeamExistsValidator(model.AwayTeamId, model.LeagueRankingId);

            var match = ObjectMapper.MapToMatch(model);
            match.DateCreated = DateTime.UtcNow;

            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();

            return match;
        }

        public async Task<Match> UpdateMatchAsync(MatchRequestDTO model, int id)
        {
            MatchRequestDTOValidator(model);

            var match = await GetMatchByIdAsync(id);

            ObjectMapper.MapToMatch(model);
            match.DateUpdated = DateTime.UtcNow;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();

            return match;
        }

        public async Task<bool> DeleteMatchAsync(int id)
        {
            var match = await GetMatchByIdAsync(id);

            if (match.IsPlayed)
            {
                if (match.HomeTeamScore > match.AwayTeamScore)
                {
                    match.HomeTeam.Points -= new WinCalculationStrategy().CalculateScores();
                }
                else if (match.HomeTeamScore < match.AwayTeamScore)
                {
                    match.AwayTeam.Points -= new WinCalculationStrategy().CalculateScores();
                }
                else
                {
                    match.HomeTeam.Points -= new DrawCalculationStrategy().CalculateScores();
                    match.AwayTeam.Points -= new DrawCalculationStrategy().CalculateScores();
                }
            }

            match.IsDeleted = true;
            match.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return match.IsDeleted;
        }

        public async Task PlayMatchAsync(MatchRequestDTO model)
        {
            MatchRequestDTOValidator(model);

            var match = await GetMatchByIdAsync(model.Id);

            if (match.IsPlayed)
            {
                throw new MatchHasAlreadyBeenPlayedException(ExceptionMessages.MatchHasAlreadyBeenPlayed);
            }

            match.HomeTeamScore = model.HomeTeamScore;
            match.AwayTeamScore = model.AwayTeamScore;
            match.IsPlayed = true;

            if (model.HomeTeamScore > model.AwayTeamScore)
            {
                match.HomeTeam.Points += new WinCalculationStrategy().CalculateScores();
            }
            else if (model.HomeTeamScore < model.AwayTeamScore)
            {
                match.AwayTeam.Points += new WinCalculationStrategy().CalculateScores();
            }
            else
            {
                match.HomeTeam.Points += new DrawCalculationStrategy().CalculateScores();
                match.AwayTeam.Points += new DrawCalculationStrategy().CalculateScores();
            }

            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MatchResponseDTO>> GetAllMatchesAsync()
        {
            var allMatches = await _context.Matches
                .Include(m => m.LeagueRanking)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Select(m => ObjectMapper.MapToMatchResponseDTO(m))
                .ToListAsync();

            return allMatches;
        }

        public async Task<List<MatchResponseDTO>> GetAllMatchesByTeamAsync(int teamId)
        {
            var allMatches = await _context.Matches
                .Include(m => m.LeagueRanking)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId) && !m.IsDeleted)
                .Select(m => ObjectMapper.MapToMatchResponseDTO(m))
                .ToListAsync();

            return allMatches;
        }

        private void MatchRequestDTOValidator(MatchRequestDTO model)
        {
            if (model is null)
            {
                throw new EntityNotFoundException(ExceptionMessages.RequestModelEmpty);
            }
        }

        private async Task<Match> GetMatchByIdAsync(int id)
        {
            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted)
                ?? throw new EntityNotFoundException(ExceptionMessages.MatchNotFound);

            return match;
        }
    }
}
