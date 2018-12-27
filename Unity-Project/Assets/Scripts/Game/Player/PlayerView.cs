using DG.Tweening;
using Game.Shared.Abstract;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerView : BaseView
    {
        public int MaxSlowMotionHits { get; set; }
        public float JumpForce { get; set; }
        public float SpeedZ { get; set; }
        public float JumpDuration { get; set; }
        public float SlowMotionJumpDuration { get; set; }

        public ReactiveCommand<GameObject> OnPlatformHit { get; private set; }
        public ReactiveCommand OnPlatformMiss { get; private set; }
        public ReactiveCommand<GameObject> OnBoostHit { get; private set; }

        private bool _isOnPlatform;
        private bool _isJumpComplete;
        private bool _gameOn;
        private float _initY;
        private bool _slowMotionBoostActivated;
        private int _numSlotMotionHit;

        private void Awake()
        {
            OnPlatformHit = new ReactiveCommand<GameObject>();
            OnPlatformMiss = new ReactiveCommand();
            OnBoostHit = new ReactiveCommand<GameObject>();
        }

        public void StartGame()
        {
            _gameOn = true;

            _initY = transform.position.y;
            Jump();
        }

        private void Jump()
        {
            Jump(SpeedZ, JumpForce);
        }

        private void Jump(float jumpLength, float jumpForce)
        {
            _isOnPlatform = false;
            _isJumpComplete = false;

            var jumpPosition = transform.position;
            jumpPosition.y = _initY;
            jumpPosition.z += jumpLength;

            var jumpDuration = SlowMotionBoostActivated ? SlowMotionJumpDuration : JumpDuration;
            transform.DOJump(jumpPosition, jumpForce, 1, jumpDuration)
                     .SetEase(Ease.Linear)
                     .OnComplete(JumpComplete);
        }

        private void JumpComplete()
        {
            _isJumpComplete = true;
            if (_isOnPlatform)
            {
                Jump();
            } else
            {
                Invoke("Die", 0.1f);
            }
        }

        private void Die()
        {
            Stop();
            OnPlatformMiss.Execute();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_gameOn)
            {
                if (other.gameObject.CompareTag(GameTag.Platform))
                {
                    OnPlatformTrigger(other);
                }

                if (other.gameObject.CompareTag(GameTag.Boost))
                {
                    OnBoostHit.Execute(other.gameObject);
                }
            }
        }

        private void OnPlatformTrigger(Collider other)
        {
            _isOnPlatform = true;
            OnPlatformHit.Execute(other.gameObject);

            if (_gameOn && _isJumpComplete)
            {
                CancelInvoke("Die");
                Jump();
            }

            if (_slowMotionBoostActivated)
            {
                _numSlotMotionHit++;
                if (_numSlotMotionHit == MaxSlowMotionHits)
                {
                    SlowMotionBoostActivated = false;
                }
            }
        }

        public void Stop()
        {
            _gameOn = false;
            transform.DOKill();
        }

        public bool SlowMotionBoostActivated
        {
            get { return _slowMotionBoostActivated; }
            set
            {
                _numSlotMotionHit = 0;
                _slowMotionBoostActivated = value;
            }
        }
    }
}