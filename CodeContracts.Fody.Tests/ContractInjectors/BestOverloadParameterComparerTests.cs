using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class BestOverloadParameterComparerTests
    {
        [Theory(DisplayName = "Проверка компаратора параметров для определения лучшей перегрузки метода")]
        [InlineAutoFixture(typeof(byte), typeof(ulong), +1)]
        [InlineAutoFixture(typeof(ulong), typeof(long), -1)]
        [InlineAutoFixture(typeof(IComparable), typeof(DateTime), -1)]
        [InlineAutoFixture(typeof(DBNull), typeof(IDisposable), 0)]
        [InlineAutoFixture(typeof(MulticastDelegate), typeof(Delegate), 1)]
        public void CompareTest(Type xType, Type yType, int compareExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadParameterComparer sut)
        {
            var x = new ParameterDefinition(moduleDefinition.ImportReference(xType));
            var y = new ParameterDefinition(moduleDefinition.ImportReference(yType));

            var compare = sut.Compare(x, y);

            Assert.Equal(compareExpected, compare);
        }
    }
}
