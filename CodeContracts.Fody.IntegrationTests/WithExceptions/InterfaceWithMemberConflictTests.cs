extern alias WithExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithExceptions::TestFoundations.IntegrationTests.Shared;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.WithExceptions
{
    public class InterfaceWithMemberConflictTests
    {
        [Fact]
        public void MethodWithConflictBaseTest()
        {
            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Success(sut => ((InterfaceWithMemberConflictBase)sut).MethodWithConflict(DayOfWeek.Friday));

            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Fail<ArgumentException>(sut => ((InterfaceWithMemberConflictBase)sut).MethodWithConflict((DayOfWeek)111));
        }

        [Fact]
        public void MethodWithConflictTest()
        {
            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Success(sut => ((InterfaceWithMemberConflict)sut).MethodWithConflict(DayOfWeek.Friday));

            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Fail<ArgumentException>(sut => ((InterfaceWithMemberConflict)sut).MethodWithConflict((DayOfWeek)111));
        }
    }
}
