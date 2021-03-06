﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.BestOverloadResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.BestOverloadResolvers
{
    public class BestOverloadCriteriaTests
    {
        [Theory(DisplayName = "Проверка применимости метода валидации для определения лучшей перегрузки метода")]
        [InlineAutoFixture("ValidateWithInt", true)]
        [InlineAutoFixture("ValidateWithLong", true)]
        [InlineAutoFixture("ValidateWithComparable", true)]
        [InlineAutoFixture("ValidateWithEnumerable", false)]
        public void IsApplyMinIntMaxIntTest(string methodName, bool isApplyExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadCriteria sut)
        {
            var parameterDefinitions = new[]
            {
                new ParameterDefinition("x", 0, moduleDefinition.TypeSystem.Int32),
                new ParameterDefinition("y", 0, moduleDefinition.TypeSystem.Int32),
            };

            var isApply = sut.IsApply(moduleDefinition.FindMethod("CustomContractWithMethodsAttribute", methodName), parameterDefinitions);

            Assert.Equal(isApplyExpected, isApply);
        }

        [Theory(DisplayName = "Проверка применимости метода валидации для определения лучшей перегрузки метода")]
        [InlineAutoFixture("ValidateWithComparable", true)]
        [InlineAutoFixture("ValidateWithInt", false)]
        [InlineAutoFixture("ValidateWithLong", false)]
        [InlineAutoFixture("ValidateWithEnumerable", false)]
        public void IsApplyMinDateTimeTest(string methodName, bool isApplyExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadCriteria sut)
        {
            var parameterDefinitions = new[]
            {
                new ParameterDefinition("x", 0, moduleDefinition.TypeSystem.Int32),
                new ParameterDefinition("y", 0, moduleDefinition.ImportReference(typeof(DateTime))),
            };

            var isApply = sut.IsApply(moduleDefinition.FindMethod("CustomContractWithMethodsAttribute", methodName), parameterDefinitions);

            Assert.Equal(isApplyExpected, isApply);
        }
    }
}
