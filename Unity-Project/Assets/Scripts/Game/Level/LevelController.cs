using Game.Boost;
using Game.Config;
using Game.Score;
using Game.Services;
using Game.Shared.Abstract;
using UniRx;
using Game.Shared.Extensions;
using Zenject;

namespace Game.Level
{
    public class LevelController : AbstractController
    {
        [Inject] private GlobalLevelConfig _globalLevelConfig;
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private MouseInputService _mouseInputService;
        [Inject] private CameraView _cameraView;
        [Inject] private LevelsConfig _levelsConfig;
        [Inject] private BoostService _boostService;

        private readonly LevelView _levelView;

        public LevelController(LevelView levelView)
        {
            _levelView = levelView;
        }

        protected override void OnInjectionsInit()
        {
            _levelView.SetupLevel(_globalLevelConfig, _levelsConfig.Levels[0]);

            SetupSubscriptions();
        }

        private void SetupSubscriptions()
        {
            _gameStateModel.State
                           .Subscribe(GameStateChange)
                           .AddTo(Disposer);

            _gameStateManager.OnGameResume
                             .Subscribe(_ => GameResume())
                             .AddTo(Disposer);

            _scoreModel.CurrentPlatformId
                       .Subscribe(HitPlatform)
                       .AddTo(Disposer);

            _mouseInputService.OnDrag
                              .Subscribe(Drag)
                              .AddTo(Disposer);

            _boostService.OnBoostActivate
                         .Subscribe(BoostActivated)
                         .AddTo(Disposer);
        }

        private void GameStateChange(GameState state)
        {
            _levelView.IsGameRunning = state == GameState.Running;
        }

        private void GameResume()
        {
            var position = _levelView.GetPlatformLocalPosition(_scoreModel.CurrentPlatformId.Value);
            _levelView.SetPositionX(-position.x);
            _levelView.ResetPlatformScale();
        }

        private void HitPlatform(int platformId)
        {
            _levelView.ShowPlatformRange(platformId);
            
            if (platformId == _levelView.NumPlatforms - 1)
            {
                HitLastPlatform(platformId);
            }
        }

        private void HitLastPlatform(int platformId)
        {
            _gameStateManager.LevelComplete();

            var effectPosition = _levelView.GetPlatformGlobalPosition(platformId);
            effectPosition.y = 2;
            EffectsManager.PlayEffect(Effects.Fireworks, effectPosition);
        }

        private void Drag(float drag)
        {
            if (_gameStateModel.State.Value == GameState.Running)
            {
                _levelView.transform.Translate(drag, 0, 0);
                _cameraView.OffsetX = drag;
            }
        }

        private void BoostActivated(BoostType boostType)
        {
            switch (boostType)
            {
                case BoostType.PlatformSizeReset:
                    _levelView.BoostPlatformsSize();
                    break;
                
                case BoostType.AlignPath:
                    _levelView.BoostAlighPath(_scoreModel.CurrentPlatformId.Value);
                    break;
            }
        }
    }
}