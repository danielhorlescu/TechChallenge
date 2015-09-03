namespace Gambling.Domain
{
    public class Bet
    {
        public int Event { get; set; }

        public int Participant { get; set; }

        public decimal Stake { get; set; }

        public decimal Win { get; set; }
    }
}