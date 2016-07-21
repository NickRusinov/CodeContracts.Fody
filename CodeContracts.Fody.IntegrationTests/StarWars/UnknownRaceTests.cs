using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.StarWars;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.StarWars
{
    public class UnknownRaceTests
    {
        [Fact(DisplayName = "Проверка свойства Sentience класса UnknownRace")]
        public void PropertySentienceTest()
        {
            ContractAssert.Fail(() => new UnknownRace(),
                ur => ur.Sentience.Get());
        }

        [Fact(DisplayName = "Проверка свойства Population класса UnknownRace")]
        public void PropertyPopulationTest()
        {
            ContractAssert.Success(() => new UnknownRace(),
                ur => ur.Population = 10000L,
                ur => ur.Population.Get());

            ContractAssert.Success(() => new UnknownRace(),
                ur => ur.Population = 0L,
                ur => ur.Population.Get());

            ContractAssert.Fail(() => new UnknownRace(),
                ur => ur.Population.Get());

            ContractAssert.Fail(() => new UnknownRace(),
                ur => ur.Population = - 10000L,
                ur => ur.Population.Get());
        }
    }
}
