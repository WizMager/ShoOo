﻿using UnityEngine;
using Views.Impl.Projectile.Impl.BaseVariants;
using Views.Impl.Projectile.Interfaces;

namespace Views.Impl.Projectile.Impl
{
    public class PistolBullet : OneTouchProjectile, IFlyable
    {
        public void Fly(float speed, Vector3 direction)
        {
            projectileRigidbody.AddForce(direction * speed, ForceMode.Acceleration);
        }
    }
}