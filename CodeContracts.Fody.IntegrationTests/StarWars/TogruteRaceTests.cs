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
    public class TogruteRaceTests
    {
        [Fact(DisplayName = "Проверка свойства Sentience класса TogrutaRace")]
        public void PropertySentienceTest()
        {
            ContractAssert.Success(() => new TogrutaRace(),
                tr => tr.Sentience.Get());
        }

        [Fact(DisplayName = "Проверка свойства Population класса TogrutaRace")]
        public void PropertyPopulationTest()
        {
            ContractAssert.Success(() => new TogrutaRace(),
                tr => tr.Population.Get());

            ContractAssert.Success(() => new TogrutaRace(),
                tr => tr.Population = 60000L,
                tr => tr.Population.Get());

            ContractAssert.Success(() => new TogrutaRace(),
                tr => tr.Population = 0L,
                tr => tr.Population.Get());

            ContractAssert.Fail(() => new TogrutaRace(),
                tr => tr.Population = - 60000L,
                tr => tr.Population.Get());
        }
    }
}
