using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractValidateResolvers
{
    public class ContractValidateScannerTests
    {
        [Theory(DisplayName = "Проверка типа исключения и сообщения об ошибке для методов валидации контракта")]
        [InlineAutoFixture("ValidateA", null, typeof(Exception))]
        [InlineAutoFixture("ValidateB", "method level", typeof(Exception))]
        [InlineAutoFixture("ValidateC", null, typeof(ArgumentNullException))]
        [InlineAutoFixture("ValidateD", "method level", typeof(ArgumentNullException))]
        public void ContractAttributeWithoutClassAttributesTest(string validateMethod, string errorMessage, Type exceptionType,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractValidateScanner sut)
        {
            var customAttribute = moduleDefinition.FindProperty("Lightsaber", "Color").CustomAttributes.First();

            var contractValidateDefinitions = sut.Scan(customAttribute).ToList();

            Assert.Equal(4, contractValidateDefinitions.Count);
            var contractValidateDefinition = Assert.Single(contractValidateDefinitions, cvd => Equals(cvd.ValidateMethod, moduleDefinition.FindMethod("RedLightsaberAttribute", validateMethod)));
            Assert.Equal(moduleDefinition.ImportReference(exceptionType).Resolve(), contractValidateDefinition.ExceptionType);
            Assert.Equal(errorMessage, contractValidateDefinition.ErrorMessage);
        }

        [Theory(DisplayName = "Проверка типа исключения и сообщения об ошибке для методов валидации контракта")]
        [InlineAutoFixture("ValidateA", "class level", typeof(ArgumentException))]
        [InlineAutoFixture("ValidateB", "method level", typeof(ArgumentException))]
        [InlineAutoFixture("ValidateC", "class level", typeof(ArgumentNullException))]
        [InlineAutoFixture("ValidateD", "method level", typeof(ArgumentNullException))]
        public void ContractAttributeWithClassAttributesTest(string validateMethod, string errorMessage, Type exceptionType,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractValidateScanner sut)
        {
            var customAttribute = moduleDefinition.FindProperty("Lightsaber", "Color").CustomAttributes.Skip(1).First();

            var contractValidateDefinitions = sut.Scan(customAttribute).ToList();

            Assert.Equal(4, contractValidateDefinitions.Count);
            var contractValidateDefinition = Assert.Single(contractValidateDefinitions, cvd => Equals(cvd.ValidateMethod, moduleDefinition.FindMethod("BlueLightsaberAttribute", validateMethod)));
            Assert.Equal(moduleDefinition.ImportReference(exceptionType).Resolve(), contractValidateDefinition.ExceptionType);
            Assert.Equal(errorMessage, contractValidateDefinition.ErrorMessage);
        }
    }
}
