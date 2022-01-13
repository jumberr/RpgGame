using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
    }
}