using System.Collections.Generic;
using System.Linq;

namespace Gambling.Domain.Aggregates
{
    public class Customer : IAggregateRoot
    {
        public Customer()
        {
            SettledBets = new List<Bet>();
            UnSettledBets = new List<Bet>();
        }

        public int Id { get; set; }

        public List<Bet> SettledBets { get; set; }

        public List<Bet> UnSettledBets { get; set; }

        public bool IsWinningAtAnUnusualRate()
        {
            if (SettledBets.Count == 0)
            {
                return false;
            }

            int betsWon = SettledBets.Count(b => b.Win > 0);
            decimal percentageWon = (decimal)betsWon/SettledBets.Count;

            return percentageWon > Constants.unusualRateThreshHold;
        }

        public void ComputeUnSettledBetsStatus()
        {
            if (IsWinningAtAnUnusualRate())
            {
                UnSettledBets.ForEach(u => u.Status.IsRisky = true);
            }

            var customerAverageBet = GetCustomerAverageBet();

            UnSettledBets.ForEach(ub => ComputeStatus(ub, customerAverageBet));
        }

        private decimal GetCustomerAverageBet()
        {
            if (SettledBets.Count == 0)
            {
                return 0;
            }
            return SettledBets.Average(s => s.Stake);
        }

        private void ComputeStatus(Bet unSettledBet, decimal customerAverageBet)
        {
            if (BetHasHighWinAmount(unSettledBet))
            {
                unSettledBet.Status.HasHighWinAmount = true;
            }

            if (!SettledBets.Any())
            {
                return;
            }

            if (StakeIsHighlyUnusual(unSettledBet, customerAverageBet))
            {
                unSettledBet.Status.IsHighlyUnusual = true;
                return;
            }

            if (StakeIsUnusual(unSettledBet, customerAverageBet))
            {
                unSettledBet.Status.IsUnusual = true;
            }
        }

        private static bool BetHasHighWinAmount(Bet unSettledBet)
        {
            return unSettledBet.Win >= Constants.highWinAmountThreshHold;
        }

        private static bool StakeIsUnusual(Bet unSettledBet, decimal customerAverageBet)
        {
            return unSettledBet.Stake > 10*customerAverageBet;
        }

        private static bool StakeIsHighlyUnusual(Bet unSettledBet, decimal customerAverageBet)
        {
            return unSettledBet.Stake > 30*customerAverageBet;
        }

        public List<Bet> GetUnusualUnSettledBets { get; set; }

        public List<Bet> GetHighRiskUnsettledBets()
        {
            return UnSettledBets.Where(u => u.HasHighRisk()).ToList();
        }
    }
}