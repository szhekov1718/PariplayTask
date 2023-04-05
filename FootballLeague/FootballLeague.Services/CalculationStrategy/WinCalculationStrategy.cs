using FootballLeague.Services.CalculationStrategy.Contract;

namespace FootballLeague.Services.CalculationStrategy
{
    public class WinCalculationStrategy : IPointsCalcuationStrategy
    {
        public int CalculateScores() => 3;
    }
}
