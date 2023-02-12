using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.Slot.Drop
{
    public interface ICanBeDragged : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public IDropArea DropArea { get; }
    }
}