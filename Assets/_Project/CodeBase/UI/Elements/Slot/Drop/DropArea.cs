using System;
using System.Collections.Generic;
using _Project.CodeBase.UI.Elements.SpecificButtonLogic;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Slot.Drop
{
    public class DropArea : MonoBehaviour
    {
        public List<DropCondition> DropConditions = new List<DropCondition>();
        public event Action<DraggableComponent> OnDropHandler;

        private void Start() => 
            DropConditions.Add(new SlotDropCondition());

        private void OnDestroy() => 
            DropConditions.Clear();

        public bool Accepts(DraggableComponent draggable) => 
            DropConditions.TrueForAll(cond => cond.Check(draggable));

        public void Drop(DraggableComponent draggable) => 
            OnDropHandler?.Invoke(draggable);
    }
}