namespace Gambling.Portal.Models
{
    public class UnSettledBetViewModel
    {
        public int CustomerId { get; set; }

        public int Event { get; set; }

        public int Participant { get; set; }

        public decimal Stake { get; set; }

        public decimal Win { get; set; }

        public bool IsRisky { get; set; }

        public bool IsUnusual { get; set; }

        public bool IsHighlyUnusual { get; set; }

        public bool HasHighWinAmount { get; set; }
    }
}