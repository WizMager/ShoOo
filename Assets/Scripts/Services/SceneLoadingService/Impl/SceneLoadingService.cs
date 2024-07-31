using System;
using Configs.SceneReferenceBase;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Services.SceneLoadingService.Impl
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly ISceneReferenceBase _sceneReferenceBase;

        private string _loadedScene;

        public SceneLoadingService(ISceneReferenceBase sceneReferenceBase)
        {
            _sceneReferenceBase = sceneReferenceBase;
        }
        
        public async UniTask LoadFromSplash(Action onSceneLoaded = null)
        {
            var levelSceneOperation = SceneManager.LoadSceneAsync(_sceneReferenceBase.ScenesList[0].name);
            
            await levelSceneOperation.ToUniTask();
            
            var gameSceneOperation = SceneManager.LoadSceneAsync(_sceneReferenceBase.MainScene.name, LoadSceneMode.Additive);
            
            await gameSceneOperation.ToUniTask();
            
            _loadedScene = _sceneReferenceBase.ScenesList[0].name;
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneReferenceBase.ScenesList[0].name));
            
            onSceneLoaded?.Invoke();
        }
        
        public async UniTask LoadScene(string scene, Action onSceneLoaded = null)
        {
            var oldSceneOperation = SceneManager.UnloadSceneAsync(_loadedScene);
            var newSceneOperation = SceneManager.LoadSceneAsync(scene);
            
            await UniTask.WhenAll(oldSceneOperation.ToUniTask(), newSceneOperation.ToUniTask());
            
            _loadedScene = scene;
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

            onSceneLoaded?.Invoke();
        }
    }
}