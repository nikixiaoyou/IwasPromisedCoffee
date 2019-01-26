using System;

namespace ggj
{
    public static class EnumExtensions
    {
        public static T GetEnum<T>(this string description) where T : struct
        {
            return (T)Enum.Parse(typeof(T), description);
        }
    }
}