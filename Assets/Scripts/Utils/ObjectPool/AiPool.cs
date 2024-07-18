using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Views.Impl.Ai;

namespace Utils.ObjectPool
{
    public class AiPool<T> where T : AAiView
    {
        private readonly Stack<T> _aiStack = new ();
        private readonly AssetReference _aiPrefab;
        private readonly Transform _poolContainerTransform;

        public AiPool(AssetReference aiPrefab, int size = 5)
        {
            var poolContainer = new GameObject($"AiPool{typeof(T).Name}Container");
            _poolContainerTransform = poolContainer.transform;
            
            _aiPrefab = aiPrefab;
            InstantiateAiAtStart(size).Forget();
        }
        
        public T GetAi()
        {
            return _aiStack.Count == 0 ? InstantiateAi() : _aiStack.Pop();
        }

        public void ReleaseAi(T ai)
        {
            ai.ResetAi();
            
            ai.transform.SetParent(_poolContainerTransform);
            
            _aiStack.Push(ai);
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
                
                _aiStack.Push(aiView);
            }
        }
        
        private T InstantiateAi()
        {
            var ai = Addressables.InstantiateAsync(_aiPrefab);
            
            return ai.Result.GetComponent<T>();
        }
    }
}