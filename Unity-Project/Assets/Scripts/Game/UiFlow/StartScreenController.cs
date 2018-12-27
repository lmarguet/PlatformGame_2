using Game.Shared.Abstract;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UiFlow
{
    public class StartScreenController : AbstractController
    {
        private readonly StartScreenView _startScreenView;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private GameStateModel _gameStateModel;

        public StartScreenController(StartScreenView startScreenView)
        {
            _startScreenView = startScreenView;
        }

        protected override void OnInjectionsInit()
        {
            _startScreenView.OnPress
                            .Subscribe(_ => OnPressScreen())
                            .AddTo(Disposer);

            _gameStateModel.State
                      .Subscribe(GameStateChange)
                      .AddTo(Disposer);
        }

        private void OnPressScreen()
        {
            _gameStateManager.StartGame();
        }

        private void GameStateChange(GameState state)
        {
            var isReady = state == GameState.Ready;
            _startScreenView.gameObject.SetActive(isReady);
        }
    }
}