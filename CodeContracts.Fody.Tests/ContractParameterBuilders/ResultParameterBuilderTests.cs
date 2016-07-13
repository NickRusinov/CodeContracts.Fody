using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterBuilders
{
    public class ResultParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя команд метода Contract.Result<T>()"), AutoFixture]
        public void ContainsCallInstructionTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] TypeReference typeReference,
            ParameterDefinition parameterDefinition,
            ResultParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(parameterDefinition).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Call, ContractReferences.Result(moduleDefinition, typeReference)), buildedInstructions.Single(), CallInstructionComparer.Instance);
        }
    }
}
