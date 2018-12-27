using Game.Score;
using Game.Shared.Abstract;
using UniRx;
using Zenject;

namespace Game.HUD
{
    public class PlayerScoreViewController : AbstractController
    {
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GameStateModel _gameStateModel;

        private readonly ScoreView _scoreView;

        public PlayerScoreViewController(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        protected override void OnInjectionsInit()
        {
            _scoreModel.Score
                       .Subscribe(ScoreChange)
                       .AddTo(Disposer);

            _gameStateModel.State
                           .Subscribe(GameStateChange)
                           .AddTo(Disposer);
        }

        private void ScoreChange(int score)
        {
            _scoreView.SetScore(score);
        }

        private void GameStateChange(GameState state)
        {
            var isRunning = state == GameState.Running;
            _scoreView.gameObject.SetActive(isRunning);
        }
    }
}