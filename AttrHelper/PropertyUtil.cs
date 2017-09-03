using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PropertyAttributeUtils
{
    public static class PropertyUtil
    {
        public static PropertyInfo GetProperty<T,TValue>(
            Expression<Func<T, TValue>> selector)
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return (PropertyInfo)((MemberExpression)body).Member;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
