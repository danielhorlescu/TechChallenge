using System.Collections.Generic;

namespace Gambling.Portal.Models
{
    public class BetsViewModel
    {
        public BetsViewModel()
        {
            RiskyCustomers = new List<CustomerViewModel>();
            HighRiskUnsettledBets = new List<UnSettledBetViewModel>();
        }

        public List<CustomerViewModel> RiskyCustomers { get; set; }

        public List<UnSettledBetViewModel> HighRiskUnsettledBets { get; set; }
    }
}