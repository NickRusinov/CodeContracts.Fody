using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Configurations;
using TinyIoC;
using Xunit;

namespace CodeContracts.Fody.Tests.Configurations
{
    public class TinyIoCConfigurationTests
    {
        [Theory(DisplayName = "Проверка правильности конфигурации контейнера внедрения зависимостей"), AutoFixture]
        public void ContainerCanResolveContractExecutor(
            ModuleWeaver moduleWeaver,
            TinyIoCConfiguration sut)
        {
            sut.Configure(moduleWeaver);
            var contractExecutor = TinyIoCContainer.Current.Resolve<IContractExecutor>();

            Assert.NotNull(contractExecutor);
        }
    }
}
