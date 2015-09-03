using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;

namespace Gambling.Domain
{
    public class Bet
    {
        public Bet()
        {
            Status = new BetStatus();
        }

        public int Event { get; set; }

        public int Participant { get; set; }

        public decimal Stake { get; set; }

        public decimal Win { get; set; }

        public BetStatus Status { get; set; }

        public bool HasHighRisk()
        {
            return Status.HasHighRisk();
        }
    }

    public class BetStatus
    {
        public bool IsRisky { get; set; }

        public bool IsUnusual { get; set; }

        public bool IsHighlyUnusual { get; set; }

        public bool HasHighWinAmount { get; set; }

        public bool HasHighRisk()
        {
            return IsRisky || IsUnusual || IsHighlyUnusual || HasHighWinAmount;
        }
    }
}