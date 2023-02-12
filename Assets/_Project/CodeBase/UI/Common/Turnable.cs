using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class Turnable : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        public void Turn(bool value)
        {
            if (value)
                _container.Activate();
            else
                _container.Deactivate();
        }
    }
}