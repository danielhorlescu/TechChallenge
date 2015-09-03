using System.Collections.Generic;
using Gambling.Persistance;
using NUnit.Framework;

namespace Gambling.IntegrationTests
{
    [TestFixture]
    public class CsvFileRepositoryTests
    {
        [Test]
        public void Should_Load_UnSettled_Bets_From_File()
        {
            IFileRepository csvFileRepositoryTests = new CsvFileRepository();

            List<BetDto> bets = csvFileRepositoryTests.LoadList<BetDto>("Unsettled.csv");
            
            Assert.IsNotNull(bets);
        }

        [Test]
        public void Should_Load_Settled_Bets_From_File()
        {
            IFileRepository csvFileRepositoryTests = new CsvFileRepository();

            List<BetDto> bets = csvFileRepositoryTests.LoadList<BetDto>("Settled.csv");

            Assert.IsNotNull(bets);
        }
    }
}