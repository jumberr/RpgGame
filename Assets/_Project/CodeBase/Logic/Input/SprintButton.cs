using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace _Project.CodeBase.Logic.Input
{
    public class SprintButton : OnScreenControl
    {
        [InputControl(layout = "Button"), SerializeField] private string _controlPath;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        public void SendValue(float value) => 
            SendValueToControl(value);
    }
}