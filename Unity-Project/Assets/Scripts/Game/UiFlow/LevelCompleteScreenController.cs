using Game.Shared.Abstract;
using UniRx;
using Zenject;

namespace Game.UiFlow
{
    public class LevelCompleteScreenController : AbstractController
    {
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private GameStateManager _gameStateManager;

        private readonly LevelCompleteScreenView _levelCompleteScreenView;

        public LevelCompleteScreenController(LevelCompleteScreenView levelCompleteScreenView)
        {
            _levelCompleteScreenView = levelCompleteScreenView;
        }

        protected override void OnInjectionsInit()
        {
            _gameStateModel.State
                           .Subscribe(GameStateChange)
                           .AddTo(Disposer);

            _levelCompleteScreenView.OnClose
                                    .Subscribe(_ => NextLevel())
                                    .AddTo(Disposer);
        }

        private void GameStateChange(GameState gameState)
        {
            var isLevelComplete = gameState == GameState.Complete;
            _levelCompleteScreenView.gameObject.SetActive(isLevelComplete);
        }

        private void NextLevel()
        {
            _gameStateManager.StartNewLevel();
        }
    }
}