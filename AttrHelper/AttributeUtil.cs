using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace PropertyAttributeUtils
{
    public static class AttributeUtil
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

        public static IDictionary<string, IList<TAttr>> GetPropetiesAttributes<T, TAttr>() where TAttr : Attribute
        {
            var propertyAttributes = new Dictionary<string, IList<TAttr>>();

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true).OfType<TAttr>().ToList(); ;
               
                propertyAttributes.Add(prop.Name, attrs);
            }

            return propertyAttributes;
        }

        public static IDictionary<string, IList<Attribute>> GetPropetiesAttributes<T>()
        {
            return GetPropetiesAttributes<T, Attribute>();
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
                var authAttr = attr as TAttr;

                if (authAttr == null) continue;

                return authAttr;

            }

            return null;
        }
    }
}
