using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace CodeContracts.Fody.Tests
{
    public class AutoFixtureAttribute : AutoDataAttribute
    {
        private static readonly string moduleFileName = "CodeContracts.TestAssembly.dll";

        private static readonly Lazy<ModuleDefinition> moduleDefinitionLazy = new Lazy<ModuleDefinition>(() => ModuleDefinition.ReadModule(moduleFileName));

        public AutoFixtureAttribute()
        {
            Fixture.Customize(new AutoConfiguredMoqCustomization());
            Fixture.Behaviors.Add(new NullRecursionBehavior());

            Fixture.Register(() => moduleDefinitionLazy.Value);
        }
    }
}
