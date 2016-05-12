using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
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
            Fixture.Register((ModuleDefinition md) => md.TypeSystem);
            Fixture.Register((ModuleDefinition md) => md.FindType("DarthMaul"));
            Fixture.Register((ModuleDefinition md) => md.FindType("DarthMaul") as TypeReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "JoinDarkSide"));
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "JoinDarkSide") as MethodReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "JoinDarkSide").CustomAttributes.First());

            Fixture.Register(() => Instruction.Create(OpCodes.Ldarg_0));
        }
    }
}
