using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
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

        private readonly Lazy<ModuleDefinition> moduleDefinitionLazy = new Lazy<ModuleDefinition>(() => ModuleDefinition.ReadModule(moduleFileName));

        public AutoFixtureAttribute()
        {
            Fixture.Customize(new AutoConfiguredMoqCustomization());
            Fixture.Customize<ModuleWeaver>(cc => cc.With(mw => mw.Config, XElement.Parse("<CodeContracts />")));

            Fixture.Behaviors.Add(new NullRecursionBehavior());
            
            Fixture.Register(() => moduleDefinitionLazy.Value);
            Fixture.Register((ModuleDefinition md) => md.TypeSystem);
            Fixture.Register((ModuleDefinition md) => md.FindType("DarthMaul"));
            Fixture.Register((ModuleDefinition md) => md.FindType("DarthMaul") as TypeReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "KillJedi"));
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "KillJedi") as MethodReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "KillJedi").CustomAttributes.First());

            Fixture.Register((ModuleDefinition md) => new ContractValidate(Fixture.Create<ContractValidateDefinition>(), Fixture.CreateMany<IParameterBuilder>(Fixture.Create<MethodDefinition>().Parameters.Count)));

            Fixture.Register(() => Instruction.Create(OpCodes.Ldarg_0));
        }
    }
}
