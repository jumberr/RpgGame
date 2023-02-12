using System;

namespace _Project.CodeBase.Logic
{
    [Serializable]
    public class MagazineData
    {
        public const int Empty = 0;
        
        public int BulletsLeft;

        public MagazineData(int bulletsLeft) => 
            BulletsLeft = bulletsLeft;
    }
}