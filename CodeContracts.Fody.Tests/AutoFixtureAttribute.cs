using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CodeContracts.Fody.BestOverloadResolvers;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractValidateResolvers;
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
        private static readonly string moduleFileName = "TestFoundations.UnitTests.dll";

        private readonly Lazy<ModuleDefinition> moduleDefinitionLazy = new Lazy<ModuleDefinition>(() => ModuleDefinition.ReadModule(moduleFileName));

        public AutoFixtureAttribute()
        {
            Fixture.Customize(new AutoConfiguredMoqCustomization());
            Fixture.Behaviors.Add(new NullRecursionBehavior());

            Fixture.Customize<ModuleWeaver>(cc => cc.With(mw => mw.Config, XElement.Parse("<CodeContracts />")));
            
            Fixture.Register(() => moduleDefinitionLazy.Value);
            Fixture.Register((ModuleDefinition md) => md.TypeSystem);
            Fixture.Register((ModuleDefinition md) => md.FindType("ConcreteClass"));
            Fixture.Register((ModuleDefinition md) => md.FindType("ConcreteClass") as TypeReference);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("ConcreteClass", "Method"));
            Fixture.Register((ModuleDefinition md) => md.FindMethod("ConcreteClass", "Method") as MethodReference);
            Fixture.Register((ModuleDefinition md) => md.FindParameter("ConcreteClass", "MethodWithParameter", "parameter"));
            Fixture.Register((ModuleDefinition md) => md.FindParameter("ConcreteClass", "MethodWithParameter", "parameter") as ParameterReference);

            Fixture.Register((ModuleDefinition md) => md.FindMethod("ConcreteClass", "MethodWithAttribute") as ICustomAttributeProvider);
            Fixture.Register((ModuleDefinition md) => md.FindMethod("ConcreteClass", "MethodWithAttribute").CustomAttributes.First());

            Fixture.Register((ModuleDefinition md) => new BestOverloadCriteria());
            Fixture.Register((ModuleDefinition md) => new BestOverloadCriteria() as IBestOverloadCriteria);
            Fixture.Register((ModuleDefinition md, MethodDefinition med, IParameterBuilder pb) => med.Parameters.Select(pd => new ContractValidateParameter(pd, pb)));
            Fixture.Register((ModuleDefinition md, MethodDefinition med, ContractValidateDefinition cvd) => new ContractValidate(cvd, Fixture.CreateMany<IParameterBuilder>(med.Parameters.Count).ToList()));

            Fixture.Register((ModuleDefinition md) => Instruction.Create(OpCodes.Nop));
        }
    }
}
