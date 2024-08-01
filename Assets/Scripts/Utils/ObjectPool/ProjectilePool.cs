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
        
        private readonly Stack<T> _projectileStack = new ();
        private readonly AssetReference _projectilePrefab;
        private readonly Transform _poolContainerTransform;
        
        public ProjectilePool(AssetReference projectilePrefab, int bulletsInShot = 1)
        {
            _projectilePrefab = projectilePrefab;
            
            var poolContainer = new GameObject($"ProjectilePool{nameof(_projectilePrefab.SubObjectName)}Container");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(bulletsInShot * PROJECTILES_MULTIPLIER).Forget();
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
                
                _projectileStack.Push(projectileView);
            }
        }
        
        private T InstantiateProjectile()
        {
            var projectile = Addressables.InstantiateAsync(_projectilePrefab);
            
            return projectile.Result.GetComponent<T>();
        }
    }
}