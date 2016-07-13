using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterBuilders
{
    public class BoxParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя команд упаковки типов значений"), AutoFixture]
        public void BoxInstructionTest(
            [Frozen] ModuleDefinition moduleDefinition)
        {
            var sut = new BoxParameterBuilder(moduleDefinition, moduleDefinition.TypeSystem.Int16);

            var buildedInstructions = sut.Build(new ParameterDefinition(moduleDefinition.TypeSystem.Object)).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Box, moduleDefinition.TypeSystem.Int16), buildedInstructions.Single(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка команд построителя команд упаковки типов значений"), AutoFixture]
        public void NoBoxInstructionTest(
            [Frozen] ModuleDefinition moduleDefinition)
        {
            var sut = new BoxParameterBuilder(moduleDefinition, moduleDefinition.TypeSystem.String);
            
            var buildedInstructions = sut.Build(new ParameterDefinition(moduleDefinition.TypeSystem.Object)).ToList();

            Assert.Empty(buildedInstructions);
        }
    }
}
