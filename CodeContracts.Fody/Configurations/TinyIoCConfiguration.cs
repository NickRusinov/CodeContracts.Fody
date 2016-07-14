using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractCleaners;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.ContractParameterParsers;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using TinyIoC;

namespace CodeContracts.Fody.Configurations
{
    /// <summary>
    /// Registers all services in dependency injection container (TinyIOC)
    /// </summary>
    public class TinyIoCConfiguration
    {
        /// <summary>
        /// Registers all services in dependency injection container
        /// </summary>
        /// <param name="moduleWeaver">ModuleWeaver instance for Fody addin</param>
        public void Configure(ModuleWeaver moduleWeaver)
        {
            var container = TinyIoCContainer.Current;
            
            container.AutoRegister(Enumerable.Repeat(Assembly.GetExecutingAssembly(), 1), DuplicateImplementationActions.RegisterMultiple, t => !t.Name.StartsWith("Composite"));

            container.Register(moduleWeaver);
            container.Register(moduleWeaver.ModuleDefinition);

            container.Register((tic, _) => tic.Resolve<IContractConfigParser>().Parse(moduleWeaver.Config.ToString()));
            container.Register((tic, _) => tic.ResolveByConfig<IContractCleaner, ContractCleaner, DisabledContractCleaner>(cc => cc.Clean));
            container.Register((tic, _) => tic.ResolveByConfig<IContractExecutor, ContractExecutor, DisabledContractExecutor>(cc => cc.IsEnabled));
            
            container.Register<IEnsuresInjector>((tic, _) => new RequiresEnsuresInvariantInjector(tic.Resolve<IContractValidateResolver>(), tic.Resolve<IInstructionsBuilder>("EnsuresContractBuilder")));
            container.Register<IRequiresInjector>((tic, _) => new RequiresEnsuresInvariantInjector(tic.Resolve<IContractValidateResolver>(), tic.Resolve<IInstructionsBuilder>("RequiresContractBuilder")));
            container.Register<IInvariantInjector>((tic, _) => new RequiresEnsuresInvariantInjector(tic.Resolve<IContractValidateResolver>(), tic.Resolve<IInstructionsBuilder>("InvariantContractBuilder")));
            
            container.Register<IInstructionsBuilder>((tic, _) => new ContractBuilder(tic.Resolve<ContractEnsuresFactory>()), "EnsuresContractBuilder");
            container.Register<IInstructionsBuilder>((tic, _) => new ContractBuilder(tic.Resolve<ContractRequiresFactory>()), "RequiresContractBuilder");
            container.Register<IInstructionsBuilder>((tic, _) => new ContractBuilder(tic.Resolve<ContractInvariantFactory>()), "InvariantContractBuilder");
            
            container.Register<IMethodParameterParser>((tic, _) => new CompositeMethodParameterParser(new LazyCollection<IMethodParameterParser>(tic.ResolveAll<IMethodParameterParser>(false).ToList)));
            container.Register<IMemberParameterParser>((tic, _) => new CompositeMemberParameterParser(new LazyCollection<IMemberParameterParser>(tic.ResolveAll<IMemberParameterParser>(false).ToList)));
            container.Register<IContractValidateParametersResolver>((tic, _) => new CompositeContractValidateParametersResolver(new LazyCollection<IContractValidateParametersResolver>(tic.ResolveAll<IContractValidateParametersResolver>(false).ToList)));
        }
    }
}
