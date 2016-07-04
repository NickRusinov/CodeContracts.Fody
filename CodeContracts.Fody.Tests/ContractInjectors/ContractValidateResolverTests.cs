﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractValidateResolverTests
    {
        [Theory(DisplayName = "Проверка вызова сканера методов валидации контрактов"), AutoFixture]
        public void ContractValidateScannerHasBeenCalledTest(
            [Frozen] Mock<IContractValidateScanner> contractValidateScannerMock,
            ContractDefinition contractDefinition,
            IReadOnlyCollection<ContractValidateParameter> contractValidateParameters,
            ContractValidateResolver sut)
        {
            sut.Resolve(contractDefinition, contractValidateParameters);

            contractValidateScannerMock.Verify(cvs => cvs.Scan(contractDefinition.ContractAttribute), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова получателя наилучшей перегрузки методов валидации контрактов"), AutoFixture]
        public void BestOverloadResolverHasBeenCalledTest(
            [Frozen] Mock<IBestOverloadResolver> bestOverloadResolverMock,
            ContractDefinition contractDefinition,
            IReadOnlyCollection<ContractValidateParameter> contractValidateParameters,
            ContractValidateResolver sut)
        {
            sut.Resolve(contractDefinition, contractValidateParameters);

            bestOverloadResolverMock.Verify(bor => bor.Resolve(It.IsAny<IReadOnlyCollection<MethodDefinition>>(), It.IsAny<IReadOnlyCollection<ParameterDefinition>>()), Times.Once);
        }
    }
}
