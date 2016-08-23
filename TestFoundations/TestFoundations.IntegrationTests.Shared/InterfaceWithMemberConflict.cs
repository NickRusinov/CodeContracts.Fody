using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public interface InterfaceWithMemberConflictBase
    {
        void MethodWithConflict([Enum] DayOfWeek parameterWithEnum);
    }

    public interface InterfaceWithMemberConflict : InterfaceWithMemberConflictBase
    {
        new void MethodWithConflict([Enum] DayOfWeek parameterWithEnum);
    }

    public class InterfaceWithMemberConflictImplementation : InterfaceWithMemberConflict
    {
        void InterfaceWithMemberConflictBase.MethodWithConflict(DayOfWeek parameterWithEnum)
        {

        }

        void InterfaceWithMemberConflict.MethodWithConflict(DayOfWeek parameterWithEnum)
        {

        }
    }
}
