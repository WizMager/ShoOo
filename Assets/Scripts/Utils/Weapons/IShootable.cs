namespace Utils.Weapons
{
    public interface IShootable
    {
        float FireRate { get; }
        
        void Shoot();
    }
}