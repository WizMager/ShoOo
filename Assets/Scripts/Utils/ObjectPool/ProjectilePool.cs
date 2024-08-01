using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Views.Impl.Projectile;

namespace Utils.ObjectPool
{
    public class ProjectilePool<T> where T : AProjectileView
    {
        private const int PROJECTILES_MULTIPLIER = 5;
        
        private readonly Queue<T> _projectileQueue = new ();
        private readonly AssetReference _projectilePrefab;
        private readonly Transform _poolContainerTransform;
        private readonly int _bulletsInShot;
        
        public ProjectilePool(AssetReference projectilePrefab, int bulletsInShot = 1)
        {
            _bulletsInShot = bulletsInShot;
            _projectilePrefab = projectilePrefab;
            
            var poolContainer = new GameObject($"ProjectilePool{nameof(_projectilePrefab.SubObjectName)}Container");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(bulletsInShot * PROJECTILES_MULTIPLIER).Forget();
        }
        
        public T GetProjectile()
        {
            if (_projectileQueue.Count <= _bulletsInShot * PROJECTILES_MULTIPLIER)
            {
                for (var i = 0; i < _bulletsInShot; i++)
                {
                    InstantiateProjectile().Forget();
                }
            }

            if (_projectileQueue.Count > 0)
            {
                return _projectileQueue.Dequeue();
            }
            else
            {
                Debug.LogError($"[{nameof(ProjectilePool<T>)}]There is no projectile in pool");
                return null;
            }
        }

        public void ReleaseProjectile(T projectile)
        {
            projectile.ResetProjectile();
            
            projectile.transform.SetParent(_poolContainerTransform);
            
            _projectileQueue.Enqueue(projectile);
        }

        private async UniTaskVoid InstantiateProjectilesAtStart(int size)
        {
            var asyncOperations = new List<AsyncOperationHandle<GameObject>>();
            
            for (var i = 0; i < size; i++)
            {
                var instantiateAsync = Addressables.InstantiateAsync(_projectilePrefab);
                
                asyncOperations.Add(instantiateAsync);
            }
            
            await UniTask.WhenAll(asyncOperations.Select(o => o.Task.AsUniTask()).ToArray());
            
            foreach (var asyncOperationHandle in asyncOperations)
            {
                var hasAAiViewComponent = asyncOperationHandle.Result.TryGetComponent(out T projectileView);
                
                if (!hasAAiViewComponent)
                {
                    throw new Exception($"[{nameof(ProjectilePool<T>)}/{nameof(_projectilePrefab.SubObjectName)}]: There is no {nameof(T)} component on {asyncOperationHandle.Result.name}");
                }
                
                projectileView.transform.SetParent(_poolContainerTransform);
                projectileView.ResetProjectile();
                
                _projectileQueue.Enqueue(projectileView);
            }
        }
        
        private async UniTaskVoid InstantiateProjectile()
        {
            var projectileOperation = Addressables.InstantiateAsync(_projectilePrefab);

            await UniTask.WhenAll(projectileOperation.ToUniTask());

            var projectileView = projectileOperation.Result.GetComponent<T>();
            
            _projectileQueue.Enqueue(projectileView);
        }
    }
}