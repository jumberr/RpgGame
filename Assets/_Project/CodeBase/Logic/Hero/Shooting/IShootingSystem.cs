using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public interface IShootingSystem
    {
        void SetupConfig<T>(T specs) where T : GunSpecs;
        void SetupFirePoint(Transform configFirePoint);
        void Shoot();
    }
}