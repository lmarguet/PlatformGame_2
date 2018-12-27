using System;
using JetBrains.Annotations;
using Zenject;

namespace Game.Shared.Abstract
{
    public abstract class AbstractController : AbstractDisposable, IDisposable
    {
        [Inject]
        [UsedImplicitly]
        private void InitInjections()
        {
            OnInjectionsInit();
        }

        protected virtual void OnInjectionsInit()
        {
        }
    }
}