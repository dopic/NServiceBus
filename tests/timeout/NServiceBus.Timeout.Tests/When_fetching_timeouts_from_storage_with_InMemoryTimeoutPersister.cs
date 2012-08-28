namespace NServiceBus.Timeout.Tests
{
    using System;
    using System.Linq;
    using Core;
    using NUnit.Framework;

    [TestFixture, Ignore]
    public class When_fetching_timeouts_from_storage_with_InMemoryTimeoutPersister : WithInMemoryTimeoutPersister
    {
        [Test]
        public void Should_only_return_timeouts_for_time_slice()
        {
            const int numberOfTimeoutsToAdd = 10;

            for (var i = 0; i < numberOfTimeoutsToAdd; i++)
            {
                persister.Add(new TimeoutData
                                  {
                                      Time = DateTime.UtcNow.AddHours(-1)
                                  });
            }

            for (var i = 0; i < numberOfTimeoutsToAdd; i++)
            {
                persister.Add(new TimeoutData
                {
                    Time = DateTime.UtcNow.AddHours(1)
                });
            }

            Assert.AreEqual(numberOfTimeoutsToAdd, GetNextChunk().Count());
        }
    }
}