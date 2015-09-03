using System.Collections.Generic;
using System.Linq;

namespace Gambling.Domain.Aggregates
{
    public class Customer : IAggregateRoot
    {
        public Customer()
        {
            SettledBetHistory = new List<Bet>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public List<Bet> SettledBetHistory { get; set; }

        public bool IsWinningAtAnUnusualRate()
        {
            if (SettledBetHistory.Count == 0)
            {
                return false;
            }

            int betsWon = SettledBetHistory.Count(b => b.Win > 0);
            decimal percentageWon = (decimal)betsWon/SettledBetHistory.Count;

            return percentageWon > Constants.unusualRateThreshHold;
        }
    }
}