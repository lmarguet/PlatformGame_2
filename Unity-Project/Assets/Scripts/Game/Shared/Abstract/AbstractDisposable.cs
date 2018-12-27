using System;
using UniRx;

namespace Game.Shared.Abstract
{
    public class AbstractDisposable
    {
        protected CompositeDisposable Disposer = new CompositeDisposable();


        public void Dispose(CompositeDisposable disposer)
        {
            if (disposer == null)
            {
                return;
            }

            disposer.Dispose();
            OnDispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose()
        {
        }

        public void Dispose()
        {
            Dispose(Disposer);
        }
    }
}