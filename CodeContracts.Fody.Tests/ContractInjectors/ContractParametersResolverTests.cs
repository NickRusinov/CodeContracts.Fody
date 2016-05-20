using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
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
        public void ConstIfParameterValueIsConst(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractMembers = sut.Resolve(contractDefinition, methodDefinition).ToList();
            var constMember = contractMembers.Skip(1).First();

            Assert.Equal(2, contractMembers.Count);
            Assert.Equal(moduleDefinition.TypeSystem.UInt64.Resolve(), constMember.ParameterDefinition.ParameterType.Resolve());
            Assert.IsType<ConstParameterBuilder>(constMember.ParameterBuilder);
            Assert.Equal("arg", constMember.ParameterDefinition.Name);
        }

        [Theory(DisplayName = "Проверка разрешения строкового параметра атрибута контракта"), AutoFixture]
        public void ParseIfParameterValueIsString(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IMethodParameterParser> methodParameterParserMock,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractMembers = sut.Resolve(contractDefinition, methodDefinition).ToList();
            var stringMember = contractMembers.First();

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, "$parameter"), Times.Once);
            Assert.Equal(2, contractMembers.Count);
            Assert.Equal("arg", stringMember.ParameterDefinition.Name);
        }
    }
}
