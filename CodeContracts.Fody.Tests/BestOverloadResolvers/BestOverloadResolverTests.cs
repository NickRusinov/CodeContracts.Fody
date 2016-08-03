using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.BestOverloadResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.BestOverloadResolvers
{
    public class BestOverloadResolverTests
    {
        [Theory(DisplayName = "Проверка выбора наилучшей версии метода валидации")]
        [InlineAutoFixture(new[] { "x", "y" }, new[] { typeof(int), typeof(int) }, "ValidateWithInt")]
        [InlineAutoFixture(new[] { "x", "y" }, new[] { typeof(long), typeof(long) }, "ValidateWithLong")]
        [InlineAutoFixture(new[] { "x", "y" }, new[] { typeof(DateTime), typeof(DateTime) }, "ValidateWithComparable")]
        public void ResolveTest(string[] parameterNames, Type[] parameterTypes, string methodNameExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadResolver sut)
        {
            var methodDefinitions = moduleDefinition.FindType("CustomContractWithMethodsAttribute").Methods.Where(md => md.IsStatic).ToList();
            var parameterDefinitions = parameterNames.Zip(parameterTypes, (s, t) => new ParameterDefinition(s, 0, moduleDefinition.ImportReference(t))).ToList();
            
            var method = sut.Resolve(methodDefinitions, parameterDefinitions);

            Assert.Equal(moduleDefinition.FindMethod("CustomContractWithMethodsAttribute", methodNameExpected), method);
        }

        [Theory(DisplayName = "Проверка выбора наилучшей версии метода валидации")]
        [InlineAutoFixture(new[] { "x" }, new[] { typeof(int) }, typeof(BestOverloadMissingMethodsException))]
        [InlineAutoFixture(new[] { "x", "y", "z" }, new[] { typeof(int), typeof(int), typeof(int) }, typeof(BestOverloadMissingMethodsException))]
        [InlineAutoFixture(new[] { "x", "y" }, new[] { typeof(short), typeof(short) }, typeof(BestOverloadAmbiguousMethodsException))]
        public void ResolveThrowsTest(string[] parameterNames, Type[] parameterTypes, Type exceptionTypeExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadResolver sut)
        {
            var methodDefinitions = moduleDefinition.FindType("CustomContractWithMethodsAttribute").Methods.Where(md => md.IsStatic).ToList();
            var parameterDefinitions = parameterNames.Zip(parameterTypes, (s, t) => new ParameterDefinition(s, 0, moduleDefinition.ImportReference(t))).ToList();

            Assert.Throws(exceptionTypeExpected, () => sut.Resolve(methodDefinitions, parameterDefinitions));
        }
    }
}
