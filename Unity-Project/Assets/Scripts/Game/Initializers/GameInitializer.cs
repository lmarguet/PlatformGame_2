using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Initializers
{
    public class GameInitializer : AbstractInitializer
    {
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private List<IDisposableGameInitializer> _initializers;
        [Inject] private GameStateManager _gameController;

        public override void Initialize()
        {
            Debug.Log("Running GameInitializer");

            _gameController.OnGameDispose.Subscribe(_ => DisposeInitializers());
            _gameController.OnGameRestart.Subscribe(_ => ReloadInitializers());
            _gameStateModel.Ready();

            RunInitializers();
        }

        private void ReloadInitializers()
        {
            Debug.Log("ReloadInitializers");
            _initializers.ForEach(initializer => initializer.ResetDisposer());

            RunInitializers();
        }

        private void RunInitializers()
        {
            _initializers.ForEach
            (
                initializer =>
                {
                    Debug.LogFormat("Running {0}", initializer);
                    initializer.Initialize();
                }
            );
        }

        private void DisposeInitializers()
        {
            _initializers.ForEach(initalizer => initalizer.Dispose());
        }
    }
}