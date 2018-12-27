using Game.Config;
using Game.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Initializers
{
    public class PlayerGameInitializer : AbstractInitializer, IDisposableGameInitializer
    {
        [Inject] private PlayerConfig _playerConfig;
        [Inject] private MiscConfig _miscConfig;
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private GameStateModel _scoreStateModel;
        [Inject] private CameraView _camera;
        private PlayerView _playerView;

        public override void Initialize()
        {
            _playerView = InstantiatePrefabInRoot(_playerConfig.PlayerPrefab);
            
            var playerController = new PlayerController(_playerView).AddTo(Disposer);
            Inject(playerController);
            
            _camera.StartFollowing(_playerView.transform);
        }

        protected override void OnDispose()
        {
           
        }
    }
}