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
        [InlineAutoFixture(new[] { "self", "arg", "min" }, new[] { typeof(object), typeof(int), typeof(int) }, "ValidateMinInt")]
        [InlineAutoFixture(new[] { "self", "arg", "min", "max" }, new[] { typeof(object), typeof(long), typeof(long), typeof(long) }, "ValidateMinMaxLong")]
        [InlineAutoFixture(new[] { "self", "arg", "min", "max" }, new[] { typeof(object), typeof(DateTime), typeof(DateTime), typeof(DateTime) }, "ValidateMinMaxComparable")]
        public void ResolveTest(string[] parameterNames, Type[] parameterTypes, string methodNameExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadResolver sut)
        {
            var methodDefinitions = moduleDefinition.FindType("MyAttribute").Methods.Where(md => md.IsStatic).ToList();
            var parameterDefinitions = parameterNames.Zip(parameterTypes, (s, t) => new ParameterDefinition(s, 0, moduleDefinition.ImportReference(t))).ToList();
            
            var method = sut.Resolve(methodDefinitions, parameterDefinitions);

            Assert.Equal(moduleDefinition.FindMethod("MyAttribute", methodNameExpected), method);
        }
    }
}
