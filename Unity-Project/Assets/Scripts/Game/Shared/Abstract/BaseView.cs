using System;
using UniRx;
using UnityEngine;

namespace Game.Shared.Abstract
{
    public class BaseView : MonoBehaviour, IDisposable
    {
        protected CompositeDisposable Disposer = new CompositeDisposable();

        public void Dispose()
        {
            if (Disposer == null)
            {
                return;
            }

            OnDispose();
            Disposer.Dispose();

            Destroy(gameObject);

            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose()
        {
        }
        
        
    }
}