using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Configs.SceneReferenceBase
{
    public interface ISceneReferenceBase
    {
        Scene MainScene { get; }
        List<Scene> ScenesList { get; }
    }
}