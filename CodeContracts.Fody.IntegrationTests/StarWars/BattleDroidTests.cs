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
    public class BattleDroidTests
    {
        [Fact(DisplayName = "Проверка свойства Sentience класса BattleDroid")]
        public void PropertySentienceTest()
        {
            ContractAssert.Fail(() => new BattleDroid(),
                bd => bd.Sentience.Get());
        }

        [Fact(DisplayName = "Проверка свойства Population класса BattleDroid")]
        public void PropertyPopulationTest()
        {
            ContractAssert.Success(() => new BattleDroid(),
                bd => bd.Population.Get());

            ContractAssert.Fail(() => new BattleDroid(),
                bd => bd.Population = - 1000000L,
                bd => bd.Population.Get());
        }

        [Fact(DisplayName = "Проверка свойства Health класса BattleDroid")]
        public void PropertyHealthTest()
        {
            ContractAssert.Success(() => new BattleDroid(),
                bd => bd.Health.Get());

            ContractAssert.Fail(() => new BattleDroid(),
                bd => bd.Health++,
                bd => bd.Health.Get());
        }

        [Fact(DisplayName = "Проверка метода FixSelf класса BattleDroid")]
        public void MethodFixSelfTest()
        {
            ContractAssert.Success(() => new BattleDroid(),
                bd => bd.FixSelf());

            ContractAssert.Fail(() => new BattleDroid(),
                bd => bd.Health = 5,
                bd => bd.FixSelf());
        }
    }
}
