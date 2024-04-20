using System;

namespace _Project.CodeBase.Utils
{
    public static class TypeRestrictedUtils
    {
        public static bool IsContainsSupportedType<T>(this T typeRestricted, Type type) where T : ITypeRestricted => 
            Array.Exists(typeRestricted.SupportedTypes, x => x == type);
    }
}