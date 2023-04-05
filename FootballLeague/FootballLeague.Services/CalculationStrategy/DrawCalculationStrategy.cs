using FootballLeague.Services.CalculationStrategy.Contract;

namespace FootballLeague.Services.CalculationStrategy
{
    public class DrawCalculationStrategy : IPointsCalcuationStrategy
    {
        public int CalculateScores() => 1;
    }
}
