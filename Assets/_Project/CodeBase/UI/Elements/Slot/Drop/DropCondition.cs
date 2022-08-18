using _Project.CodeBase.UI.Elements.SpecificButtonLogic;

namespace _Project.CodeBase.UI.Elements.Slot.Drop
{
    public abstract class DropCondition
    {
        public abstract bool Check(DraggableComponent draggable);
    }
}