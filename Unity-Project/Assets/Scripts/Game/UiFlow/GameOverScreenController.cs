using Game.Shared.Abstract;
using UniRx;
using Zenject;

namespace Game.UiFlow
{
    public class GameOverScreenController : AbstractController
    {
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private GameStateManager _gameStateManager;

        private readonly GameOverScreenView _gameOverScreenView;

        public GameOverScreenController(GameOverScreenView gameOverScreenView)
        {
            _gameOverScreenView = gameOverScreenView;
        }

        protected override void OnInjectionsInit()
        {
            _gameStateModel.State
                           .Subscribe(GameStateChange)
                           .AddTo(Disposer);

            _gameOverScreenView.OnContinue
                               .Subscribe(_ => Continue())
                               .AddTo(Disposer);

            _gameOverScreenView.OnRetry
                               .Subscribe(_ => Retry())
                               .AddTo(Disposer);
        }

        private void GameStateChange(GameState state)
        {
            var isDead = state == GameState.Dead;
            _gameOverScreenView.gameObject.SetActive(isDead);
        }

        private void Continue()
        {
            _gameStateManager.Continue();
        }

        private void Retry()
        {
            _gameStateManager.Restart();
        }
    }
}