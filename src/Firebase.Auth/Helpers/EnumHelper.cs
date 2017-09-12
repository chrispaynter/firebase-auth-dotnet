using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Firebase.Auth.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// The Firebase API isn't great at returning error types, in fact some look like this
        /// "INVALID_PROVIDER_ID : Provider Id is not supported.". In order to strongly type
        /// these errors, this method will look for an enum value where the error string starts
        /// with that enum values EnumMember value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T GetValueIfStringStartsWith<T>(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return default(T);
            }

            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var customAtts = enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true);
                if (customAtts == null || !customAtts.Any())
                {
                    continue;
                }
                var enumMemberAttribute = ((EnumMemberAttribute[])customAtts).Single();
                if (str.Contains(enumMemberAttribute.Value)) return (T)Enum.Parse(enumType, name);
            }
            //throw exception or whatever handling you want or
            return default(T);
        }
    }
}
