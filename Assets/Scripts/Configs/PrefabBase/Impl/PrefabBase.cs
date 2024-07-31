using System;
using System.Collections.Generic;
using Services.EnemySpawnService;
using UnityEngine;

namespace Configs.PrefabBase.Impl
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(PrefabBase), fileName = nameof(PrefabBase))]
    public class PrefabBase : ScriptableObject, IPrefabBase
    {
        [SerializeField] private List<AiPrefabVo> aiPrefabs;
        
        public GameObject GetAiPrefabWithType(EAiType aiType)
        {
            foreach (var aiPrefab in aiPrefabs)
            {
                if (aiPrefab.aiType != aiType) continue;

                return aiPrefab.prefab;
            }
            
            throw new Exception($"[{typeof(PrefabBase)}]: Ai prefab with type {aiType} is not found!");
        }
    }
}