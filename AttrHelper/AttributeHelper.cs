using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace PropertyAttributeHelper
{
    public static class AttributeHelper
    {
        public static TAttr GetPropertyAttributeValue<T, TValue, TAttr>(
           Expression<Func<T, TValue>> selector) where TAttr : Attribute
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var propInfo = (PropertyInfo)((MemberExpression)body).Member;

                    var attrs = propInfo.GetCustomAttributes(true);

                    foreach (object attr in attrs)
                    {
                        if (attr is TAttr) return (TAttr)attr;
                    }

                    return null;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static IDictionary<string, TAttr> GetPropetiesAttributes<T, TAttr>() where TAttr : Attribute
        {
            var propertyAttributes = new Dictionary<string, TAttr>();

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);

                foreach (object attr in attrs)
                {
                    var authAttr = (TAttr)attr;

                    if (authAttr == null) continue;

                    var propName = prop.Name;

                    propertyAttributes.Add(propName, authAttr);

                }
            }

            return propertyAttributes;

        }

        public static TAtrr GetClassAttributeValue<T, TAtrr>() where TAtrr : Attribute
        {
            var attributes = typeof(T).GetCustomAttributes(typeof(TAtrr), true);

            if (attributes.Length == 0) return null;

            return (TAtrr)attributes[0];
        }

        public static TAttr GetAttributeFromPropInfo<TAttr>(PropertyInfo propertyInfo) where TAttr : Attribute
        {
            var attrs = propertyInfo.GetCustomAttributes(true);

            foreach (var attr in attrs)
            {
                var authAttr = (TAttr)attr;

                if (authAttr == null) continue;

                return authAttr;

            }

            return null;
        }
    }
}
