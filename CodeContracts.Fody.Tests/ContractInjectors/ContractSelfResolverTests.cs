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
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractSelfResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения нулевой ссылки на текущий экземпляр параметра атрибута контракта для статического метода"), AutoFixture]
        public void ResolveStaticMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractSelfResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Sith", "Create");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractMembers = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var contractMember = Assert.Single(contractMembers);
            Assert.IsType<NullParameterBuilder>(contractMember.ParameterBuilder);
            Assert.Equal("self", contractMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.TypeSystem.Void.Resolve(), contractMember.ParameterDefinition.ParameterType.Resolve());
        }

        [Theory(DisplayName = "Проверка разрешения ссылки на текущий экземпляр параметра атрибута контракта для нестатического метода"), AutoFixture]
        public void ResolveInstanceMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractSelfResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractMembers = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var contractMember = Assert.Single(contractMembers);
            Assert.IsType<ThisParameterBuilder>(contractMember.ParameterBuilder);
            Assert.Equal("self", contractMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.FindType("DarthMaul"), contractMember.ParameterDefinition.ParameterType.Resolve());
        }
    }
}
