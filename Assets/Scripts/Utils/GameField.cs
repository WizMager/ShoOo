using System.Collections.Generic;
using System.Linq;
using Alchemy.Inspector;
using UnityEngine;
using Views;
using Views.Impl;
using Views.Impl.Ai;

namespace Utils
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private List<AAiView> aiViews;
        [SerializeField] private AView playerCharacterView;
        [SerializeField] private List<EnemySpawner.EnemySpawner> enemySpawners;
        
        public PlayerCharacterView PlayerCharacterView => playerCharacterView as PlayerCharacterView;
        public List<EnemySpawner.EnemySpawner> EnemySpawners => enemySpawners;
        
        public IViewInitializable[] AllViewInitializables
        {
            get
            {
                var list = new List<IViewInitializable>();
                list.Add(playerCharacterView);

                return list.ToArray();
            }
        }
        
        public IAi[] AllAiViewInitializables
        {
            get
            {
                var list = new List<IAi>();
                
                list.AddRange(aiViews);

                return list.ToArray();
            }
        }
        
        [Button]
        public virtual void AutoFill()
        {
            ClearAllFields();

            playerCharacterView = FindObjectOfType<PlayerCharacterView>();

            aiViews = new List<AAiView>(FindObjectsOfType<AAiView>().ToArray());
            
            Debug.Log("Autofill Complete");
        }

        private void ClearAllFields()
        {
            playerCharacterView= null;
        }
    }
}