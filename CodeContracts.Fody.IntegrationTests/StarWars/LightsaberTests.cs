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
            ContractAssert.Success(() => new Lightsaber(LightsaberColor.Blue, 1));

            ContractAssert.Fail(() => new Lightsaber((LightsaberColor)42, 1));
        }
    }
}
