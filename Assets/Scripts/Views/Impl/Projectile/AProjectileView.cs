namespace Views.Impl.Projectile
{
    public class AProjectileView : AView
    {
        public virtual void ResetProjectile()
        {
            gameObject.SetActive(false);
        }

        public void ActivateProjectile()
        {
            gameObject.SetActive(true);
        }
    }
}