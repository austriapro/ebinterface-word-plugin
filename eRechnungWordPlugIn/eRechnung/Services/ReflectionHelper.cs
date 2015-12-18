using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Validation.Properties;

namespace eRechnung.Services
{
    public class ReflectionHelper
    {
        public static PropertyInfo GetProperty(Type type, string propertyName, bool throwIfInvalid)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            PropertyInfo propertyInfo = type.GetProperty(propertyName); // type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (!IsValidProperty(propertyInfo))
            {
                if (throwIfInvalid)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.ExceptionInvalidProperty,
                            propertyName,
                            type.FullName));
                }
                else
                {
                    return null;
                }
            }

            return propertyInfo;
        }

        internal static bool IsValidProperty(PropertyInfo propertyInfo)
        {
            return null != propertyInfo				// exists
                    && propertyInfo.CanRead			// and it's readable
                    && propertyInfo.CanWrite
                    && propertyInfo.GetIndexParameters().Length == 0;	// and it's not an indexer
        } 
    }
}