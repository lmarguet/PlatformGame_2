using Game.Shared.Abstract;
using UniRx;

namespace Game.Score
{
    public class ScoreModel : AbstractModel
    {
        public readonly ReadOnlyReactiveProperty<int> Score;
        private readonly ReactiveProperty<int> _score;

        public readonly ReadOnlyReactiveProperty<int> CurrentPlatformId;
        private readonly ReactiveProperty<int> _currentPlatformId;

        public ScoreModel()
        {
            _score = new ReactiveProperty<int>();
            Score = _score.ToReadOnlyReactiveProperty();

            _currentPlatformId = new ReactiveProperty<int>();
            CurrentPlatformId = _currentPlatformId.ToReadOnlyReactiveProperty();
        }

        public void Reset()
        {
            _score.Value = 0;
            _currentPlatformId.Value = 0;
        }

        public void SetProgress(int platformId)
        {
            _currentPlatformId.Value = platformId;
            IncrementScore();
        }

        private void IncrementScore()
        {
            _score.Value++;
        }
    }
}