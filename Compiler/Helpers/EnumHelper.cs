using System;
using System.Linq;

namespace Compiler.Helpers
{
    public class EnumHelper
    {
        public static bool TryGetValue<T>(object obj, out T result) where T : struct
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var values = (T[])Enum.GetValues(typeof(T));
            var val = (T)Enum.ToObject(typeof(T), obj);
            if (!values.Contains(val))
            {
                result = default(T);
                return false;
            }

            result = val;
            return true;
        }
    }
}
