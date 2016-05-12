using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Tests.Internal
{
    internal static class ReflectionExtensions
    {
        public static TField FindPrivateField<TField>(this object instance, string fieldName)
        {
            return (TField)instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance);
        }
    }
}
