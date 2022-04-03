using _Project.CodeBase.Logic.Weapon;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "Data", menuName = "Static Data/Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public WeaponSettings WeaponSettings;
        public Weapon Weapon;
        public Magazine Magazine;
    }
}