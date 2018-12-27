using Game.Config;
using Game.Player;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Test
{
    public class TestSceneRoot : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GlobalLevelConfig _globalLevelConfig;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private CameraView _cameraView;

        private float _lastPosition;

        void Start()
        {
            _playerView.JumpForce = _playerConfig.JumpForce;
            _playerView.SpeedZ = _playerConfig.SpeedZ;

            _playerView.OnPlatformHit.Subscribe(_ => PrintPlayerPosition());

            _cameraView.StartFollowing(_playerView.transform);

            _playerView.StartGame();
        }

        private void PrintPlayerPosition()
        {
            var posZ = _playerView.transform.position.z;
            var jumpLength = posZ - _lastPosition;
            Debug.LogFormat("Jump length: {0}", jumpLength);
            _lastPosition = posZ;
        }
    }
}