﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Views.Impl.Ai;

namespace Utils.ObjectPool
{
    public class AiPool<T> where T : AAiView
    {
        private readonly Queue<T> _aiViews = new ();
        private readonly AssetReference _aiPrefab;
        private readonly Transform _poolContainerTransform;
        private readonly ReactiveCommand<AAiView[]> _startInstantiateFinishedCommand = new();
        
        public bool IsReady => _aiViews.Count > 0;
        public Observable<AAiView[]> StartInstantiateFinished => _startInstantiateFinishedCommand;

        public AiPool(AssetReference aiPrefab, int size = 1)
        {
            var poolContainer = new GameObject($"AiPool{typeof(T).Name}Container");
            _poolContainerTransform = poolContainer.transform;
            
            _aiPrefab = aiPrefab;
            InstantiateAiAtStart(size).Forget();
        }
        
        public (bool isNewAi, T aiView) GetAi()
        {
            if (_aiViews.Count <= 0)
                return (true, InstantiateAi());
            
            var projectile = _aiViews.Dequeue();
            
            projectile.transform.SetParent(null);
                
            return (false, projectile);
        }

        public void ReleaseAi(T ai)
        {
            ai.ResetAi();
            
            ai.transform.SetParent(_poolContainerTransform);
            
            _aiViews.Enqueue(ai);
        }

        private async UniTaskVoid InstantiateAiAtStart(int size)
        {
            var asyncOperationAi = new List<AsyncOperationHandle<GameObject>>();
            
            for (var i = 0; i < size; i++)
            {
                var instantiateAsync = Addressables.InstantiateAsync(_aiPrefab);
                
                asyncOperationAi.Add(instantiateAsync);
            }
            
            await UniTask.WhenAll(asyncOperationAi.Select(o => o.Task.AsUniTask()).ToArray());
            
            foreach (var asyncOperationHandle in asyncOperationAi)
            {
                var hasAAiViewComponent = asyncOperationHandle.Result.TryGetComponent(out T aiView);
                
                if (!hasAAiViewComponent)
                {
                    throw new Exception($"[{nameof(AiPool<T>)}]: There is no {typeof(T).Name} component on {asyncOperationHandle.Result.name}");
                }
                
                aiView.transform.SetParent(_poolContainerTransform);
                
                aiView.ResetAi();
                
                _aiViews.Enqueue(aiView);
            }
            
            _startInstantiateFinishedCommand?.Execute(_aiViews.ToArray());
        }
        
        private T InstantiateAi()
        {
            var instantiateAsyncOperation = Addressables.InstantiateAsync(_aiPrefab);

            return instantiateAsyncOperation.Result.GetComponent<T>();
        }
    }
}