using System;

namespace _Project.CodeBase.Utils
{
    public interface ITypeRestricted
    {
        Type[] SupportedTypes { get; }
    }
}