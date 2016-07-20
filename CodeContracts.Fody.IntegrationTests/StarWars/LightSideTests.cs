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
    public class LightSideTests
    {
        [Fact(DisplayName = "Проверка конструктора класса LightSide")]
        public void ConstructorTest()
        {
            ContractAssert.Success(() => new LightSide());

            ContractAssert.Fail(() => new LightSide(caption: null));

            ContractAssert.Fail(() => new LightSide(alternationCaption: null));
        }

        [Fact(DisplayName = "Проверка свойства Caption класса LightSide")]
        public void PropertyCaptionTest()
        {
            ContractAssert.Success(() => new LightSide(),
                ls => ls.Caption.Get());
        }

        [Fact(DisplayName = "Проверка свойства AlternativeCaption класса LightSide")]
        public void PropertyAlternativeCaptionTest()
        {
            ContractAssert.Success(() => new LightSide(),
                ls => ls.AlternativeCaption.Get());

            ContractAssert.Success(() => new LightSide(),
                ls => ls.AlternativeCaption = "Ashla",
                ls => ls.AlternativeCaption.Get());

            ContractAssert.Fail(() => new LightSide(),
                ls => ls.AlternativeCaption = null);
        }

        [Fact(DisplayName = "Проверка метода CreateLightsaber класса LightSide")]
        public void MethodCreateLightsaberTest()
        {
            ContractAssert.Success(() => new LightSide(),
                ls => ls.CreateLightsaber(LightsaberColor.Blue));

            ContractAssert.Fail(() => new LightSide(),
                ls => ls.CreateLightsaber(LightsaberColor.Red));
        }
    }
}
