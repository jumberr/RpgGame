namespace _Project.CodeBase.Logic.Weapon
{
    public class Magazine
    {
        public int size;
        public int bulletsLeft;

        public Magazine(int size, int bulletsLeft)
        {
            this.size = size;
            this.bulletsLeft = bulletsLeft;
        }
    }
}