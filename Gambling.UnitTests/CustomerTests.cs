using System.Collections.Generic;
using Gambling.Domain;
using Gambling.Domain.Aggregates;
using NUnit.Framework;

namespace Gambling.UnitTests
{
    public class CustomerTests
    {
        public Customer CreateSut()
        {
            return new Customer();
        }

        [TestFixture]
        private class When_Winning_At_Un_Unsual_Rate : CustomerTests
        {

            [Test]
            public void Should_Return_False_If_Customer_Does_Not_Have_Settled_Bets()
            {
                bool isWinningAtAnUnusualRate = CreateSut().IsWinningAtAnUnusualRate();

                Assert.IsFalse(isWinningAtAnUnusualRate);
            }

            [Test]
            public void Should_Return_False_If_Customer_Has_Won_On_Less_Than_60_percent_of_times()
            {
                Customer customer = CreateSut();
                customer.SettledBetHistory = new List<Bet>
                {
                    new Bet { Win = 0 }, new Bet { Win = 0 }, new Bet { Win = 0 },
                    new Bet { Win = 5 }, new Bet { Win = 20 }, new Bet { Win = 30 }
                };

                bool isWinningAtAnUnusualRate = customer.IsWinningAtAnUnusualRate();

                Assert.IsFalse(isWinningAtAnUnusualRate);
            }

            [Test]
            public void Should_Return_False_If_Customer_Has_Won_60_percent_of_times()
            {
                Customer customer = CreateSut();
                customer.SettledBetHistory = new List<Bet>
                {
                    new Bet { Win = 0 }, new Bet { Win = 0 }, new Bet { Win = 30},
                    new Bet { Win = 5 }, new Bet { Win = 20 }
                };

                bool isWinningAtAnUnusualRate = customer.IsWinningAtAnUnusualRate();

                Assert.IsFalse(isWinningAtAnUnusualRate);
            }

            [Test]
            public void Should_Return_True_If_Customer_Has_Won_more_than_60_percent_of_times()
            {
                Customer customer = CreateSut();
                customer.SettledBetHistory = new List<Bet>
                {
                    new Bet { Win = 0 }, new Bet { Win = 40 }, new Bet { Win = 30},
                    new Bet { Win = 5 }, new Bet { Win = 20 }
                };

                bool isWinningAtAnUnusualRate = customer.IsWinningAtAnUnusualRate();

                Assert.True(isWinningAtAnUnusualRate);
            }
        }
    }
}