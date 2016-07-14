using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Configurations;
using CodeContracts.Fody.ContractCleaners;
using TinyIoC;
using Xunit;

namespace CodeContracts.Fody.Tests.Configurations
{
    public class TinyIoCConfigurationTests
    {
        [Theory(DisplayName = "Проверка правильности конфигурации контейнера внедрения зависимостей"), AutoFixture]
        public void ContainerCanResolveContractExecutorTest(
            ModuleWeaver moduleWeaver,
            TinyIoCConfiguration sut)
        {
            sut.Configure(moduleWeaver);
            var contractExecutor = TinyIoCContainer.Current.Resolve<IContractExecutor>();

            Assert.IsType<ContractExecutor>(contractExecutor);
        }

        [Theory(DisplayName = "Проверка правильности конфигурации контейнера внедрения зависимостей"), AutoFixture]
        public void ContainerResolvesDisabledContractExecutorTest(
            ModuleWeaver moduleWeaver,
            TinyIoCConfiguration sut)
        {
            sut.Configure(moduleWeaver);
            TinyIoCContainer.Current.Register(new ContractConfig { IsEnabled = false });
            var contractExecutor = TinyIoCContainer.Current.Resolve<IContractExecutor>();

            Assert.IsType<DisabledContractExecutor>(contractExecutor);
        }

        [Theory(DisplayName = "Проверка правильности конфигурации контейнера внедрения зависимостей"), AutoFixture]
        public void ContainerResolveDisabledContractCleanerTest(
            ModuleWeaver moduleWeaver,
            TinyIoCConfiguration sut)
        {
            sut.Configure(moduleWeaver);
            TinyIoCContainer.Current.Register(new ContractConfig { Clean = false });
            var contractCleaner = TinyIoCContainer.Current.Resolve<IContractCleaner>();

            Assert.IsType<DisabledContractCleaner>(contractCleaner);
        }
    }
}
