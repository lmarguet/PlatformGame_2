using System;
using Game.Shared.Abstract;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Initializers
{
    public abstract class AbstractInitializer : AbstractDisposable, IInitializable
    {
        [Inject] private SceneContext _sceneContext;

        public abstract void Initialize();

        protected T InstantiatePrefabInRoot<T>(T prefab) where T : Object, IDisposable
        {
            return Object.Instantiate(prefab, _sceneContext.transform).AddTo(Disposer);
        }

        public void ResetDisposer()
        {
            Dispose();
            Disposer = new CompositeDisposable();
        }

        protected T Inject<T>(T injectable)
        {
            _sceneContext.Container.Inject(injectable);
            return injectable;
        }
    }
}