using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Views.Impl.Ai;

namespace Utils.ObjectPool
{
    public class AiPool<T> where T : AAiView
    {
        private readonly Stack<T> _aiStack = new ();
        private readonly AssetReference _aiPrefab;

        public AiPool(AssetReference aiPrefab, int size = 5)
        {
            _aiPrefab = aiPrefab;
            for (var i = 0; i < size; i++)
            {
                var aiView = InstantiateAi();
                _aiStack.Push(aiView);
            }
        }
        
        public T GetAi()
        {
            return _aiStack.Count == 0 ? InstantiateAi() : _aiStack.Pop();
        }

        public void ReleaseAi(T ai)
        {
            _aiStack.Push(ai);
        }

        private T InstantiateAi()
        {
            var ai = Addressables.InstantiateAsync(_aiPrefab);
            
            return ai.Result.GetComponent<T>();
        }
    }
}