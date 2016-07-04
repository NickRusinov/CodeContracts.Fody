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
            Fixture.Behaviors.Add(new NullRecursionBehavior());

            Fixture.Customize<ModuleWeaver>(cc => cc.With(mw => mw.Config, XElement.Parse("<CodeContracts />")));
            
            Fixture.Register(() => moduleDefinitionLazy.Value);
            Fixture.Register((ModuleDefinition md) => md.TypeSystem);
            Fixture.Register((ModuleDefinition md) => md.FindType("DarthMaul"));
            Fixture.Register((ModuleDefinition md) => md.FindType("DarthMaul") as TypeReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "KillJedi"));
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "KillJedi") as MethodReference);
            Fixture.Register((ModuleDefinition md) => md.FindParameter("DarthMaul", "KillJedi", "jedi"));
            Fixture.Register((ModuleDefinition md) => md.FindParameter("DarthMaul", "KillJedi", "jedi") as ParameterReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("DarthMaul", "KillJedi").CustomAttributes.First());

            Fixture.Register((ModuleDefinition md) => new BestOverloadCriteria());
            Fixture.Register((ModuleDefinition md) => new BestOverloadCriteria() as IBestOverloadCriteria);
            Fixture.Register((ModuleDefinition md, ContractValidateDefinition cvd) => new ContractValidate(cvd, Fixture.CreateMany<IParameterBuilder>(md.FindMethod("DarthMaul", "KillJedi").Parameters.Count).ToList()));
            Fixture.Register((ModuleDefinition md, IParameterBuilder pb) => new[] { new ContractValidateParameter(md.FindParameter("DarthMaul", "KillJedi", "jedi"), pb), new ContractValidateParameter(md.FindParameter("DarthMaul", "KillJedi", "revenge"), pb) } as IReadOnlyCollection<ContractValidateParameter>);

            Fixture.Register(() => Instruction.Create(OpCodes.Nop));
        }
    }
}
