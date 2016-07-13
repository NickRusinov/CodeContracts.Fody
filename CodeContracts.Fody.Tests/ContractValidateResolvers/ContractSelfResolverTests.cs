using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractValidateResolvers
{
    public class ContractSelfResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения нулевой ссылки на текущий экземпляр параметра атрибута контракта для статического метода"), AutoFixture]
        public void ResolveStaticMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            RequiresDefinition requiresDefinition,
            ContractSelfResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Sith", "Create");

            var contractValidateParameters = sut.Resolve(requiresDefinition, methodDefinition).ToList();

            var contractMember = Assert.Single(contractValidateParameters);
            Assert.IsType<NullParameterBuilder>(contractMember.ParameterBuilder);
            Assert.Equal("self", contractMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.TypeSystem.Void, contractMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
        }

        [Theory(DisplayName = "Проверка разрешения нулевой ссылки на текущий экземпляр параметра атрибута контракта для конструктора"), AutoFixture]
        public void ResolveConstructorMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            RequiresDefinition requiresDefinition,
            ContractSelfResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Sith", ".ctor");

            var contractValidateParameters = sut.Resolve(requiresDefinition, methodDefinition).ToList();

            var contractMember = Assert.Single(contractValidateParameters);
            Assert.IsType<NullParameterBuilder>(contractMember.ParameterBuilder);
            Assert.Equal("self", contractMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.TypeSystem.Void, contractMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
        }

        [Theory(DisplayName = "Проверка разрешения ссылки на текущий экземпляр параметра атрибута контракта для нестатического метода"), AutoFixture]
        public void ResolveInstanceMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractSelfResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var contractMember = Assert.Single(contractValidateParameters);
            Assert.IsType<ThisParameterBuilder>(contractMember.ParameterBuilder);
            Assert.Equal("self", contractMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.FindType("DarthMaul"), contractMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
        }
    }
}
