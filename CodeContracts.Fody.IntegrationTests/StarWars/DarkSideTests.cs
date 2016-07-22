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
    public class DarkSideTests
    {
        [Fact(DisplayName = "Проверка конструктора класса DarkSide")]
        public void ConstructorTest()
        {
            ContractAssert.Success(() => new DarkSide(1));

            ContractAssert.Fail(() => new DarkSide(11));
        }

        [Fact(DisplayName = "Проверка свойства Caption класса DarkSide")]
        public void PropertyCaptionTest()
        {
            ContractAssert.Success(() => new DarkSide(1),
                ds => ds.Caption.Get());
        }

        [Fact(DisplayName = "Проверка метода CreateLightsaber класса DarkSide")]
        public void MethodCreateLightsaberTest()
        {
            ContractAssert.Success(() => new DarkSide(1),
                ds => ds.CreateLightsaber(LightsaberColor.Red));

            ContractAssert.Fail(() => new DarkSide(1),
                ds => ds.CreateLightsaber((LightsaberColor)66));

            ContractAssert.Fail(() => new DarkSide(2),
                ds => ds.CreateLightsaber(LightsaberColor.Red));

            ContractAssert.Fail(() => new DarkSide(1),
                ds => ds.CreateLightsaber(LightsaberColor.White));
        }
    }
}
