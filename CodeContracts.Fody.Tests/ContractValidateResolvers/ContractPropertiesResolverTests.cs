﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractParameterParsers;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractValidateResolvers
{
    public class ContractPropertiesResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения константного свойства атрибута контракта"), AutoFixture]
        public void ConstIfPropertyValueIsConstTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractPropertiesResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthPlagueis", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractMembers = sut.Resolve(contractDefinition, methodDefinition).ToList();

            Assert.Equal(2, contractMembers.Count);
            var constMember = Assert.Single(contractMembers, cm => cm.ParameterDefinition.Name == "Max");
            Assert.Equal(moduleDefinition.TypeSystem.Int16, constMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<CompositeParameterBuilder>(constMember.ParameterBuilder);
        }

        [Theory(DisplayName = "Проверка разрешения строкового свойства атрибута контракта"), AutoFixture]
        public void ParseIfPropertyValueIsStringTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IMethodParameterParser> methodParameterParserMock,
            ContractPropertiesResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthPlagueis", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractMembers = sut.Resolve(contractDefinition, methodDefinition).ToList();

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, "$.value"), Times.Once);
            Assert.Single(contractMembers, cm => cm.ParameterDefinition.Name == "Min");
            Assert.Equal(2, contractMembers.Count);
        }
    }
}