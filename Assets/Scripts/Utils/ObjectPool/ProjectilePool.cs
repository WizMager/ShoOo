using System.Collections.Generic;
using UnityEngine;
using Views.Impl.Projectile;

namespace Utils.ObjectPool
{
    public class ProjectilePool<T> where T : AProjectileView
    {
        private const int PROJECTILES_MULTIPLIER = 5;
        
        private readonly Stack<T> _projectileStack = new ();
        private readonly GameObject _projectilePrefab;
        private readonly Transform _poolContainerTransform;
        
        public ProjectilePool(GameObject projectilePrefab, int bulletsInShot = 1)
        {
            _projectilePrefab = projectilePrefab;
            
            var poolContainer = new GameObject($"ProjectilePool{nameof(_projectilePrefab.name)}Container");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(bulletsInShot * PROJECTILES_MULTIPLIER);
        }
        
        public T GetProjectile()
        {
            return  _projectileStack.Count == 0 ? InstantiateProjectile() : _projectileStack.Pop();
        }

        public void ReleaseProjectile(T projectile)
        {
            projectile.ResetProjectile();
            
            projectile.transform.SetParent(_poolContainerTransform);
            
            _projectileStack.Push(projectile);
        }

        private void InstantiateProjectilesAtStart(int size)
        {
            for (var i = 0; i < size; i++)
            {
                var projectile = InstantiateProjectile();
                
                projectile.transform.SetParent(_poolContainerTransform);
                projectile.ResetProjectile();
                
                _projectileStack.Push(projectile);
            }
        }
        
        private T InstantiateProjectile()
        {
            var projectile = UnityEngine.Object.Instantiate(_projectilePrefab);
            
            return projectile.GetComponent<T>();
        }
    }
}