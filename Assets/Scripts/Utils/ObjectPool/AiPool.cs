using System.Collections.Generic;
using UnityEngine;
using Views.Impl.Ai;
using Object = UnityEngine.Object;

namespace Utils.ObjectPool
{
    public class AiPool<T> where T : AAiView
    {
        private readonly Stack<T> _aiStack = new ();
        private readonly GameObject _aiPrefab;
        private readonly Transform _poolContainerTransform;
        
        public bool IsReady => _aiStack.Count > 0;

        public AiPool(GameObject aiPrefab, int size = 5)
        {
            var poolContainer = new GameObject($"AiPool{typeof(T).Name}Container");
            _poolContainerTransform = poolContainer.transform;
            
            _aiPrefab = aiPrefab;
            InstantiateAiAtStart(size);
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

        private void InstantiateAiAtStart(int size)
        {
            for (var i = 0; i < size; i++)
            {
                var aiView = InstantiateAi();
                
                aiView.transform.SetParent(_poolContainerTransform);
                aiView.ResetAi();
                
                _aiStack.Push(aiView);
            }
        }
        
        private T InstantiateAi()
        {
            var ai = Object.Instantiate(_aiPrefab);
            
            return ai.GetComponent<T>();
        }
    }
}