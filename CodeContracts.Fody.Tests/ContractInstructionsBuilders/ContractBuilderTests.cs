﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.ContractValidateResolvers;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInstructionsBuilders
{
    public class ContractBuilderTests
    {
        [Theory(DisplayName = "Проверка вызова фабрики создания построителя метода валидации контракта для построителя контракта"), AutoFixture]
        public void ContractMethodFactoryHasBeenCalledTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IContractMethodFactory> contractMethodFactoryMock,
            ContractValidate contractValidate,
            ContractBuilder sut)
        {
            var instructions = sut.Build(contractValidate).ToList();

            contractMethodFactoryMock.Verify(cmf => cmf.Create(contractValidate.ValidateDefinition.ExceptionType, contractValidate.ValidateDefinition.ErrorMessage), Times.Once);
            Assert.NotEmpty(instructions);
        }
    }
}
