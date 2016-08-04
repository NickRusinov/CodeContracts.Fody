using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CodeContracts
{
    [Conditional("CONTRACTS_FULL")]
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public class PropertyAttribute : ContractAttribute
    {
        public PropertyAttribute() { }

        public PropertyAttribute(object arg) { }

        [Pure]
        public static bool Validate(Expression arg) => 
            ((arg as LambdaExpression)?.Body as MemberExpression)?.Member is PropertyInfo && 
            ((arg as LambdaExpression)?.Body as MemberExpression)?.Expression is ParameterExpression;
    }
}
