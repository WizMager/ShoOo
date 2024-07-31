using System;
using Cysharp.Threading.Tasks;

namespace Services.SceneLoadingService
{
    public interface ISceneLoadingService
    {
        UniTask LoadFromSplash(Action onSceneLoaded = null);
        UniTask LoadScene(string scene, Action onSceneLoaded = null);
    }
}