using R3;
using UnityEngine;

namespace Views.Modules
{
    public abstract class AModule : MonoBehaviour
    {
        public virtual void Initialize(AView view, CompositeDisposable disposable)
        {
        }
    }
}