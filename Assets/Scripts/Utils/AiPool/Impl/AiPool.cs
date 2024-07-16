using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Views.Impl.Ai;

namespace Utils.AiPool.Impl
{
    public class AiPool<T> : IAiPool where T : AAiView
    {
        private readonly List<T> _aiList = new ();

        public AiPool(int size, string prefab)
        {
            for (var i = 0; i < size; i++)
            {
                var ai = Addressables.InstantiateAsync(prefab);
                var aiView = ai.Result.GetComponent<T>();
                _aiList.Add(aiView);
            }
        }
        
        public AAiView GetAi()
        {
            if (_aiList.Count == 0)
            {
                return null;
            }
            
            return null;
        }

        public void ReleaseAi(AAiView ai)
        {
            
        }

        private T InstantiateAi(string prefab)
        {
            return null;
        }
    }
}