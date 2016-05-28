using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests
{
    public class AssemblyTests
    {
        [Fact(DisplayName = "Проверка корректности загрузки тестовой сборки")]
        public void AssemblyTest()
        {
            Assembly.LoadFrom("CodeContracts.StarWars.dll");
        }
    }
}
