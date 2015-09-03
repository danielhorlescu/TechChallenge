using System.Collections.Generic;
using System.Linq;
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
                customer.SettledBets = new List<Bet>
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
                customer.SettledBets = new List<Bet>
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
                customer.SettledBets = new List<Bet>
                {
                    new Bet { Win = 0 }, new Bet { Win = 40 }, new Bet { Win = 30},
                    new Bet { Win = 5 }, new Bet { Win = 20 }
                };

                bool isWinningAtAnUnusualRate = customer.IsWinningAtAnUnusualRate();

                Assert.True(isWinningAtAnUnusualRate);
            }
        }

        [TestFixture]
        private class When_Computing_Unsettled_Bets_Status : CustomerTests
        {
            [Test]
            public void Should_Flag_Unsettled_Bet_With_Risky()
            {
                Customer customer = CreateSut();
                customer.SettledBets = new List<Bet>
                {
                    new Bet { Stake = 10, Win = 0 }, new Bet {Stake = 20,  Win = 20 }, new Bet {Stake = 30,  Win = 30},
                };

                customer.UnSettledBets = new List<Bet>
                {
                    new Bet { Stake = 110, Win = 0 }
                };

                customer.ComputeUnSettledBetsStatus();

                BetStatus betStatus = customer.UnSettledBets.First().Status;

                Assert.IsTrue(betStatus.IsRisky);
            }

            [Test]
            public void Should_Flag_Unsettled_Bet_With_Unusual()
            {
                Customer customer = CreateSut();
                customer.SettledBets = new List<Bet>
                {
                    new Bet { Stake = 10, Win = 0 }, new Bet {Stake = 20,  Win = 20 }, new Bet {Stake = 30,  Win = 30},
                };

                customer.UnSettledBets = new List<Bet>
                {
                    new Bet { Stake = 201, Win = 0 }
                };

                customer.ComputeUnSettledBetsStatus();

                BetStatus betStatus = customer.UnSettledBets.First().Status;

                Assert.IsTrue(betStatus.IsUnusual);
                Assert.IsFalse(betStatus.IsHighlyUnusual);
            }

            [Test]
            public void Should_Flag_Unsettled_Bet_With_Highly_Unusual()
            {
                Customer customer = CreateSut();
                customer.SettledBets = new List<Bet>
                {
                    new Bet { Stake = 10, Win = 0 }, new Bet {Stake = 20,  Win = 20 }, new Bet {Stake = 30,  Win = 30},
                };

                customer.UnSettledBets = new List<Bet>
                {
                    new Bet { Stake = 601, Win = 0 }
                };

                customer.ComputeUnSettledBetsStatus();

                BetStatus betStatus = customer.UnSettledBets.First().Status;

                Assert.IsTrue(betStatus.IsHighlyUnusual);
                Assert.IsFalse(betStatus.IsUnusual);
            }

            [Test]
            public void Should_Flag_Unsettled_Bet_With_Highly_Win_Rate()
            {
                Customer customer = CreateSut();

                customer.UnSettledBets = new List<Bet>
                {
                    new Bet { Stake = 601, Win = 1000 }
                };

                customer.ComputeUnSettledBetsStatus();

                BetStatus betStatus = customer.UnSettledBets.First().Status;

                Assert.IsTrue(betStatus.HasHighWinAmount);
            }

            [Test]
            public void Should_Not_Flag_Unsettled_Bet_With_HighlyUnusual_If_Customer_Has_No_Bet_History()
            {
                Customer customer = CreateSut();

                customer.UnSettledBets = new List<Bet>
                {
                    new Bet { Stake = 601, Win = 5000 }
                };

                customer.ComputeUnSettledBetsStatus();

                BetStatus betStatus = customer.UnSettledBets.First().Status;

                Assert.IsFalse(betStatus.IsHighlyUnusual);
            }
        }
    }
}