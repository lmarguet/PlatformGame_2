using Game.Boost;
using Game.Config;
using Game.Level;
using Game.Score;
using Game.Services;
using Game.Shared.Abstract;
using UniRx;
using UnityEngine;
using Game.Shared.Extensions;
using Zenject;

namespace Game.Player
{
    public class PlayerController : AbstractController
    {
        [Inject] private PlayerConfig _playerConfig;
        [Inject] private GlobalLevelConfig _globalLevelConfig;
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private BoostService _boostService;

        private readonly PlayerView _playerView;
        private float _lastPlatformZ;

        public PlayerController(PlayerView playerView)
        {
            _playerView = playerView;
        }

        protected override void OnInjectionsInit()
        {
            SetupPlayerConfig();
            SetupSubscriptions();
        }

        private void SetupPlayerConfig()
        {
            _playerView.JumpForce = _playerConfig.JumpForce;
            _playerView.SpeedZ = _playerConfig.SpeedZ;
            _playerView.JumpDuration = _playerConfig.JumpDuration;
            _playerView.SlowMotionJumpDuration = _globalLevelConfig.BoostSlowMoJumpDuration;
            _playerView.MaxSlowMotionHits = _globalLevelConfig.MaxSlowmotionHits;
        }

        private void SetupSubscriptions()
        {
            _gameStateModel.State
                           .Subscribe(GameStateChange)
                           .AddTo(Disposer);

            _gameStateManager.OnGameResume
                             .Subscribe(_ => GameResume())
                             .AddTo(Disposer);
            _playerView.OnPlatformHit
                       .Subscribe(HitPlatform)
                       .AddTo(Disposer);

            _playerView.OnPlatformMiss
                       .Subscribe(_ => PlatformMiss())
                       .AddTo(Disposer);

            _playerView.OnBoostHit
                       .Subscribe(BoostHit)
                       .AddTo(Disposer);

            _boostService.OnBoostActivate
                         .Subscribe(BoostActivated)
                         .AddTo(Disposer);
        }

        private void HitPlatform(GameObject platform)
        {
            _lastPlatformZ = platform.transform.parent.position.z;
            _scoreModel.SetProgress(platform.transform.parent.GetComponent<PlatformView>().Index);

            EffectsManager.PlayEffect
            (
                Effects.Splash,
                _playerView.transform.position,
                platform.transform
            );
        }

        private void PlatformMiss()
        {
            _gameStateManager.PlayerDied();
        }

        private void GameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Running:
                    _playerView.StartGame();
                    break;
                case GameState.Complete:
                    LevelComplete();
                    break;
                case GameState.Dead:
                    _playerView.SlowMotionBoostActivated = false;
                    break;
            }
        }

        private void GameResume()
        {
            _playerView.SetPositionZ(_lastPlatformZ);
        }

        private void LevelComplete()
        {
            _playerView.Stop();
            _playerView.SetPositionZ(_lastPlatformZ);
        }

        private void BoostHit(GameObject boost)
        {
            var boostType = boost.GetComponent<BoostScript>().BoostType;
            _boostService.ActivateBoost(boostType);
            Object.Destroy(boost);
        }

        private void BoostActivated(BoostType boostType)
        {
            switch (boostType)
            {
                case BoostType.SlowMotion:
                    _playerView.SlowMotionBoostActivated = true;
                    break;
            }
            
            EffectsManager.PlayEffect(Effects.BoostPickup, _playerView.transform.position);
        }
    }
}