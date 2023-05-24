using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Services
{
    public interface IUIFactory
    {
        Transform UIRoot { get; }
        ActorUI ActorUI { get; }
        InventoryWindow InventoryWindow { get; }

        UniTask CreateUI();
        void ShowDeathScreen();
    }
}