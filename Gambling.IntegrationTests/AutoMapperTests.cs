using AutoMapper;
using Gambling.Portal;
using NUnit.Framework;

namespace Gambling.IntegrationTests
{
    [TestFixture]
    public class AutoMapperTests
    {
        [Test]
        public void Assert_Configuration_Is_Valid()
        {
            Mapper.Initialize(c => { c.AddProfile(new PresentationMappingProfile()); });

            Mapper.AssertConfigurationIsValid();
        }
    }
}