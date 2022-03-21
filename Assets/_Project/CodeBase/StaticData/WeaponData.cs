using _Project.CodeBase.Logic.Weapon;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public Weapon Weapon;
        public Magazine Magazine;
    }
}