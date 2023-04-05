namespace FootballLeague.Services.Constants
{
    public static class ExceptionMessages
    {
        public const string TeamNotFound = "Such team does not exist!";
        public const string TeamAlreadyExists = "Team with this name already exists!";
        public const string TeamNotFoundInTheLeague = "There is no such team in this league!";

        public const string MatchNotFound = "Such match does not exist!";
        public const string MatchHasAlreadyBeenPlayed = "The match has already been played!";

        public const string LeagueRankingNotFound = "Such league ranking does not exist!";
        public const string LeagueRankingAlreadyExists = "League ranking with this name already exists!";

        public const string RequestModelEmpty = "Request model cannot be empty.";
    }
}
