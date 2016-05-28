using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Configurations;
using Xunit;

namespace CodeContracts.Fody.Tests.Configurations
{
    public class LoggerConfigurationTests
    {
        [Theory(DisplayName = "Проверка правильности вызова методов логгера"), AutoFixture]
        public void LoggerHasBeenConfiguredTest(
            ModuleWeaver moduleWeaver,
            LoggerConfiguration sut)
        {
            string testValue = null;
            moduleWeaver.LogDebug += s => testValue = "Debug:" + s;
            moduleWeaver.LogInfo += s => testValue = "Info:" + s;
            moduleWeaver.LogWarning += s => testValue = "Warning:" + s;
            moduleWeaver.LogError += s => testValue = "Error:" + s;

            sut.Configure(moduleWeaver);

            Logger.Current.LogDebug("message");
            Assert.Equal("Debug:message", testValue);

            Logger.Current.LogInfo("message");
            Assert.Equal("Info:message", testValue);

            Logger.Current.LogWarning("message");
            Assert.Equal("Warning:message", testValue);

            Logger.Current.LogError("message");
            Assert.Equal("Error:message", testValue);
        }
    }
}
