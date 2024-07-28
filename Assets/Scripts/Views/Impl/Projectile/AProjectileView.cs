namespace Views.Impl.Projectile
{
    public class AProjectileView : AView
    {
        protected float Damage;
        
        public virtual void ResetProjectile()
        {
            gameObject.SetActive(false);
        }

        public void ActivateProjectile(float projectileDamage)
        {
            Damage = projectileDamage;
            
            gameObject.SetActive(true);
        }
    }
}