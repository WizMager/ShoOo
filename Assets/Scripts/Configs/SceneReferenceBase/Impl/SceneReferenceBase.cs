using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Configs.SceneReferenceBase.Impl
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(SceneReferenceBase), fileName = nameof(SceneReferenceBase))]
    public class SceneReferenceBase : ScriptableObject, ISceneReferenceBase
    {
        [SerializeField] private Scene mainScene;
        [SerializeField] private List<Scene> scenesList;

        public List<Scene> ScenesList => scenesList;

        public Scene MainScene => mainScene;
    }
}