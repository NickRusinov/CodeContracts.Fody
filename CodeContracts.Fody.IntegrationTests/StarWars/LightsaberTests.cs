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
    public class LightsaberTests
    {
        [Fact(DisplayName = "Проверка конструктора класса Lightsaber")]
        public void ConstructorTest()
        {
            ContractAssert.Success(() => new Lightsaber());

            ContractAssert.Fail(() => new Lightsaber(color: (LightsaberColor)42));

            ContractAssert.Fail(() => new Lightsaber(bladeCount: 0));

            ContractAssert.Fail(() => new Lightsaber(bladeCount: 3));
        }

        [Fact(DisplayName = "Проверка свойства Color класса Lightsaber")]
        public void PropertyColorTest()
        {
            ContractAssert.Success(() => new Lightsaber(),
                ls => ls.Color.Get());

            ContractAssert.Fail(() => new Lightsaber(),
                ls => ls.Color = (LightsaberColor)66);
        }

        [Fact(DisplayName = "Проверка свойства BladeCount класса Lightsaber")]
        public void PropertyBladeCountTest()
        {
            ContractAssert.Success(() => new Lightsaber(),
                ls => ls.BladeCount.Get());

            ContractAssert.Fail(() => new Lightsaber(),
                ls => ls.BladeCount = 4);
        }
    }
}
