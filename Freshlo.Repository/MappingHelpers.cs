using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlo.Repository
{
    public class MappingHelpers
    {
        public static byte[] ByteArrayGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return value as byte[];
        }

        public static DateTime? DateTimeGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return Convert.ToDateTime(value);
        }

        public static int? IntegerGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return Convert.ToInt32(value);
        }

        public static decimal? DecimalGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return Convert.ToDecimal(string.Format("{0:0.00}", value));
        }

        public static long? LongGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return Convert.ToInt64(value);
        }
        public static char? CharGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return Convert.ToChar(value);
        }
        public static TimeSpan? TimeSpanGetValue(object value)
        {
            if (value == DBNull.Value)
                return null;
            return TimeSpan.Parse(value.ToString());
        }
        public static object SetNullableValue(object value)
        {
            if (value == null)
                return (object)DBNull.Value;
            return value;
        }

        public static T GenericReferenceType<T>(object value, T defaultValue = default(T)) where T : class
        {
            T castedValue;
            try
            {
                castedValue = Convert.ChangeType(value, typeof(T)) as T;
                if (castedValue == null)
                {
                    castedValue = defaultValue;
                }
            }
            catch (Exception)
            {
                castedValue = defaultValue;
            }

            return castedValue;
        }


        public static T GenericValueType<T>(object value, T defaultValue = default(T)) where T : struct
        {
            T castedValue;
            try
            {
                castedValue = (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                castedValue = defaultValue;
            }

            return castedValue;
        }
    }
}
