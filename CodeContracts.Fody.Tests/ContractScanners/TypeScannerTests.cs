using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractScanners;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractScanners
{
    public class TypeScannerTests
    {
        [Theory(DisplayName = "Проверка сканирования всех методов типа"), AutoFixture]
        public void ScanAllMethodsTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMethodScanner> methodScannerMock,
            TypeScanner sut)
        {
            sut.Scan(moduleDefinition.FindType("ConcreteClass")).ToList();

            methodScannerMock.Verify(ms => ms.Scan(It.IsAny<MethodDefinition>()), Times.Exactly(7));
        }

        [Theory(DisplayName = "Проверка сканирования всех свойств типа"), AutoFixture]
        public void ScanAllPropertiesTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IPropertyScanner> propertyScannerMock,
            TypeScanner sut)
        {
            sut.Scan(moduleDefinition.FindType("ConcreteClassWithProperty")).ToList();

            propertyScannerMock.Verify(ps => ps.Scan(It.IsAny<PropertyDefinition>()), Times.Exactly(2));
        }

        [Theory(DisplayName = "Проверка сканирования всех полей типа"), AutoFixture]
        public void ScanAllFieldsTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IFieldScanner> fieldScannerMock,
            TypeScanner sut)
        {
            sut.Scan(moduleDefinition.FindType("ConcreteClassWithField")).ToList();

            fieldScannerMock.Verify(fs => fs.Scan(It.IsAny<FieldDefinition>()), Times.Exactly(2));
        }
    }
}
