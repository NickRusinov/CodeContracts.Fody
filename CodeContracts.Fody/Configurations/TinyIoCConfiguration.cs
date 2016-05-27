using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using TinyIoC;

namespace CodeContracts.Fody.Configurations
{
    public class TinyIoCConfiguration
    {
        public void Configure(ModuleWeaver moduleWeaver)
        {
            var container = TinyIoCContainer.Current;
            
            container.AutoRegister(Enumerable.Repeat(Assembly.GetExecutingAssembly(), 1), DuplicateImplementationActions.RegisterMultiple, t => !t.Name.StartsWith("Composite"));

            container.Register(moduleWeaver);
            container.Register(moduleWeaver.ModuleDefinition);
            container.Register(moduleWeaver.LogInfo, "LogInfo");
            container.Register(moduleWeaver.LogDebug, "LogDebug");
            container.Register(moduleWeaver.LogWarning, "LogWarning");

            container.Register((tic, _) => tic.Resolve<IContractConfigParser>().Parse(moduleWeaver.Config.Value));
            
            container.Register<IEnsuresInjector>((tic, _) => new ContractInjector(tic.Resolve<IContractValidatesResolver>(), tic.Resolve<IInstructionsBuilder>("EnsuresContractBuilder")));
            container.Register<IRequiresInjector>((tic, _) => new ContractInjector(tic.Resolve<IContractValidatesResolver>(), tic.Resolve<IInstructionsBuilder>("RequiresContractBuilder")));
            container.Register<IInvariantInjector>((tic, _) => new ContractInjector(tic.Resolve<IContractValidatesResolver>(), tic.Resolve<IInstructionsBuilder>("InvariantContractBuilder")));

            container.Register<IInstructionsBuilder, ContractValidateBuilder>();
            container.Register<IInstructionsBuilder>((tic, _) => new ContractBuilder(tic.Resolve<IContractMethodFactory>("ContractEnsuresFactory")), "EnsuresContractBuilder");
            container.Register<IInstructionsBuilder>((tic, _) => new ContractBuilder(tic.Resolve<IContractMethodFactory>("ContractRequiresFactory")), "RequiresContractBuilder");
            container.Register<IInstructionsBuilder>((tic, _) => new ContractBuilder(tic.Resolve<IContractMethodFactory>("ContractInvariantFactory")), "InvariantContractBuilder");
            
            container.Register<IMethodParameterParser>((tic, _) => new CompositeMethodParameterParser(tic.ResolveAll<IMethodParameterParser>(false)));
            container.Register<IMemberParameterParser>((tic, _) => new CompositeMemberParameterParser(tic.ResolveAll<IMemberParameterParser>(false)));

            container.Register<IContractValidatesResolver>((tic, _) => new ContractValidatesResolver(tic.Resolve<IContractValidateResolver>(), tic.Resolve<IContractMembersResolver>("ContractParametersResolver"), new[] { tic.Resolve<IContractMembersResolver>("ContractSelfResolver"), tic.Resolve<IContractMembersResolver>("ContractPropertiesResolver") }));
        }
    }
}
