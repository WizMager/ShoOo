using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using Views.Modules;
using Exception = System.Exception;

namespace Views
{
    public abstract class AView : MonoBehaviour, IViewInitializable, IDisposable 
    {
        [SerializeField] protected List<AModule> modules = new ();

        protected readonly CompositeDisposable Disposable = new ();
        
        public virtual void Initialize()
        {
            foreach (var module in modules)
            {
                module.Initialize(this, Disposable);
            }
        }

        public T GetModule<T>() where T : AModule //TODO: change to Try Get Module
        {
            foreach (var module in modules)
            {
                if (module is T needModule)
                {
                    return needModule;
                }
            }
            
            throw new Exception($"[{name}]: Does not have module with type {typeof(T)}");
        }
        
        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}