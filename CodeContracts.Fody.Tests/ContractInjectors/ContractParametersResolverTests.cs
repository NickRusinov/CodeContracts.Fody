﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractParametersResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения константного параметра атрибута контракта"), AutoFixture]
        public void ConstIfParameterValueIsConstTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();
            var constMember = contractValidateParameters.Skip(1).First();

            Assert.Equal(2, contractValidateParameters.Count);
            Assert.Equal("arg", constMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.TypeSystem.UInt64, constMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<CompositeParameterBuilder>(constMember.ParameterBuilder);
        }

        [Theory(DisplayName = "Проверка разрешения строкового параметра атрибута контракта"), AutoFixture]
        public void ParseIfParameterValueIsStringTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IMethodParameterParser> methodParameterParserMock,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();
            var stringMember = contractValidateParameters.First();

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, "$parameter"), Times.Once);
            Assert.Equal(2, contractValidateParameters.Count);
            Assert.Equal("arg", stringMember.ParameterDefinition.Name);
        }
    }
}
