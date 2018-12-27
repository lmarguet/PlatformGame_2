using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerJumpBehaviour : MonoBehaviour
    {
        public ReactiveCommand<Collider> OnCollide { get; private set; }
        public ReactiveCommand OnJumpComplete { get; private set; }

        private float _initY;

        private void Awake()
        {
            _initY = transform.position.y;
            OnCollide = new ReactiveCommand<Collider>();
            OnJumpComplete = new ReactiveCommand();
        }

        public void Jump(float jumpLength, float jumpForce)
        {
            if (IsOnPlatform)
            {
                IsOnPlatform = false;

                var jumpPosition = transform.position;
                jumpPosition.y = _initY;
                jumpPosition.z += jumpLength;

                transform.DOLocalJump(jumpPosition, jumpForce, 1, 0.6f)
                         .OnComplete(JumpComplete(jumpLength, jumpForce));
            }
        }

        private TweenCallback JumpComplete(float jumpLength, float jumpForce)
        {
            return () =>
            {
                OnJumpComplete.Execute();
                Jump(jumpLength, jumpForce);
            };
        }

        public bool IsOnPlatform { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            OnCollide.Execute(other);
        }

        public void Stop()
        {
        }
    }
}