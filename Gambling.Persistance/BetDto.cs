namespace Gambling.Persistance
{
    public class BetDto
    {
        public int CustomerId { get; set; }

        public int Event { get; set; }

        public int Participant { get; set; }

        public decimal Stake { get; set; }

        public decimal Win { get; set; }
    }
}