using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Gambling.Domain;
using Gambling.Domain.Aggregates;
using Gambling.Persistance;
using Gambling.Portal.Models;

namespace Gambling.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public HomeController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public ActionResult Index()
        {
            List<Customer> customers = GetCustomers();
            customers.ForEach(c => c.ComputeUnSettledBetsStatus());

            BetsViewModel betsViewModel = MapBetsViewModel(customers);
            return View(betsViewModel);
        }

        private BetsViewModel MapBetsViewModel(List<Customer> customers)
        {
            var betsViewModel = new BetsViewModel();
            betsViewModel.RiskyCustomers =
                customers.Where(c => c.IsWinningAtAnUnusualRate()).OrderBy(c => c.Id)
                    .Select(c => new CustomerViewModel {Id = c.Id})
                    .ToList();

            foreach (Customer customer in customers.OrderBy(c => c.Id))
            {
                List<UnSettledBetViewModel> unSettledBetViewModels =
                    customer.GetHighRiskUnsettledBets().Select(ub => Mapper.Map<UnSettledBetViewModel>(ub)).ToList();
                unSettledBetViewModels.ForEach(ub => ub.CustomerId = customer.Id);
                betsViewModel.HighRiskUnsettledBets.AddRange(unSettledBetViewModels);
            }

            return betsViewModel;
        }

        private List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            MapSettledBets(customers);
            MapUnSettledBets(customers);
            return customers;
        }

        private void MapUnSettledBets(List<Customer> customers)
        {
            List<BetDto> unSettledBets =
                _fileRepository.LoadList<BetDto>(HttpContext.Server.MapPath("~/bin/Unsettled.csv"));
            foreach (BetDto unSettledBet in unSettledBets)
            {
                Customer customer = customers.SingleOrDefault(c => c.Id == unSettledBet.CustomerId);
                if (customer == null)
                {
                    customer = new Customer {Id = unSettledBet.CustomerId};
                    customers.Add(customer);
                }
                customer.UnSettledBets.Add(Mapper.Map<Bet>(unSettledBet));
            }
        }

        private void MapSettledBets(List<Customer> customers)
        {
            List<BetDto> settledBets = _fileRepository.LoadList<BetDto>(HttpContext.Server.MapPath("~/bin/Settled.csv"));
            foreach (BetDto settledBet in settledBets)
            {
                Customer customer = customers.SingleOrDefault(c => c.Id == settledBet.CustomerId);
                if (customer == null)
                {
                    customer = new Customer {Id = settledBet.CustomerId};
                    customers.Add(customer);
                }

                customer.SettledBets.Add(Mapper.Map<Bet>(settledBet));
            }
        }
    }
}