namespace Views.Impl.Projectile.Impl.BaseVariants
{
    public class OneTouchProjectile : TouchProjectile
    {
        protected override void OnTouchAdditionalActions()
        {
            ExistProjectileEndedCommand.Execute(default);
        }
    }
}