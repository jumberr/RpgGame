using System.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Task<GameObject> CreateHero(GameObject at);
    }
}