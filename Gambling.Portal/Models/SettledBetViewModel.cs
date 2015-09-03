namespace Gambling.Portal.Models
{
    public class SettledBetViewModel
    {
        public int Event { get; set; }

        public int Participant { get; set; }

        public decimal Stake { get; set; }

        public decimal Win { get; set; }

    }
}