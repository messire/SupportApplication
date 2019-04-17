using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;

namespace SupportApplication.Tests.Extensions
{
    public class FixtureExtesion
    {
        /// <summary>
        /// Creates OmitOnRecursionBehavior as opposite to ThrowingRecursionBehavior.
        /// </summary>
        /// <returns></returns>
        public static Fixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
