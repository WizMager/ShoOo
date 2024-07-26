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
        private readonly Stack<T> _projectileStack = new ();
        private readonly AssetReference _projectilePrefab;
        private readonly Transform _poolContainerTransform;
        
        public bool IsReady => _projectileStack.Count > 0;
        
        public ProjectilePool(AssetReference projectilePrefab, int size = 5)
        {
            _projectilePrefab = projectilePrefab;
            
            var poolContainer = new GameObject($"ProjectilePool{nameof(_projectilePrefab.SubObjectName)}Container");
            _poolContainerTransform = poolContainer.transform;
            
            InstantiateProjectilesAtStart(size).Forget();
        }
        
        public T GetProjectile()
        {
            return _projectileStack.Count == 0 ? InstantiateProjectile() : _projectileStack.Pop();
        }

        public void ReleaseProjectile(T aProjectile)
        {
            aProjectile.ResetProjectile();
            
            aProjectile.transform.SetParent(_poolContainerTransform);
            
            _projectileStack.Push(aProjectile);
        }

        private async UniTaskVoid InstantiateProjectilesAtStart(int size)
        {
            var asyncOperationAi = new List<AsyncOperationHandle<GameObject>>();
            
            for (var i = 0; i < size; i++)
            {
                var instantiateAsync = Addressables.InstantiateAsync(_projectilePrefab);
                
                asyncOperationAi.Add(instantiateAsync);
            }
            
            await UniTask.WhenAll(asyncOperationAi.Select(o => o.Task.AsUniTask()).ToArray());
            
            foreach (var asyncOperationHandle in asyncOperationAi)
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