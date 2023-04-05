using System;

namespace FootballLeague.Services.Exceptions
{
    public class MatchHasAlreadyBeenPlayedException : Exception
    {
        public MatchHasAlreadyBeenPlayedException(string message) : base(message)
        {
            
        }
    }
}
