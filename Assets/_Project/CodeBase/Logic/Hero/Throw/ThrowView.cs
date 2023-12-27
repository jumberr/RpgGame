using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class ThrowView : MonoBehaviour
    {
        [field: SerializeField] public Transform ThrowPosition { get; private set; }
        [field: SerializeField] public Vector3 ThrowDirection { get; private set; } = new Vector3(0, 1, 0);
    }
}