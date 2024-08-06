using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Views.Impl.Projectile;

namespace Utils.ObjectPool
{
    public class ProjectilePool<T> where T : AProjectileView
    {
        private readonly Queue<T> _projectileViews = new ();
        private readonly AssetReference _projectilePrefab;
        private readonly Transform _poolContainerTransform;
        private readonly ReactiveCommand<T[]> _projectileInstantiatedCommand = new ();

        public Observable<T[]> ProjectileInstantiated => _projectileInstantiatedCommand;
        
        public ProjectilePool(AssetReference projectilePrefab, int magazineSize, int bulletsInShot = 1)
        {
            _projectilePrefab = projectilePrefab;
            
            var poolContainer = new GameObject($"ProjectilePool{nameof(_projectilePrefab.SubObjectName)}Container");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(bulletsInShot * magazineSize).Forget();
        }
        
        public (bool isNewProjectile, T projectileView) GetProjectile()
        {
            if (_projectileViews.Count <= 0) 
                return (true, InstantiateProjectile());
            
            var projectile = _projectileViews.Dequeue();
            
            projectile.transform.SetParent(null);
                
            return (false, projectile);
        }

        public void ReleaseProjectile(T projectile)
        {
            projectile.transform.SetParent(_poolContainerTransform);
            
            _projectileViews.Enqueue(projectile);
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
                
                _projectileViews.Enqueue(projectileView);
            }

            _projectileInstantiatedCommand?.Execute(_projectileViews.ToArray());
        }
        
        private T InstantiateProjectile()
        {
            var projectileOperation = Addressables.InstantiateAsync(_projectilePrefab);
            
            return projectileOperation.Result.GetComponent<T>();
        }
    }
}