extern alias NotClean;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotClean::TestFoundations.IntegrationTests.Shared;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.NotClean
{
    public class InterfaceWithMemberConflictTests
    {
        [Fact]
        public void MethodWithConflictBaseTest()
        {
            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Success(sut => ((InterfaceWithMemberConflictBase)sut).MethodWithConflict(DayOfWeek.Friday));

            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Fail(sut => ((InterfaceWithMemberConflictBase)sut).MethodWithConflict((DayOfWeek)111));
        }

        [Fact]
        public void MethodWithConflictTest()
        {
            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Success(sut => ((InterfaceWithMemberConflict)sut).MethodWithConflict(DayOfWeek.Friday));

            ContractAssert.With<InterfaceWithMemberConflictImplementation>()
                .Fail(sut => ((InterfaceWithMemberConflict)sut).MethodWithConflict((DayOfWeek)111));
        }
    }
}
