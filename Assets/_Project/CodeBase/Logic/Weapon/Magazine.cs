namespace _Project.CodeBase.Logic.Weapon
{
    public class Magazine
    {
        public int _size;
        public int _bulletsLeft;

        public Magazine(int size, int bulletsLeft)
        {
            this._size = size;
            this._bulletsLeft = bulletsLeft;
        }
    }
}