using Game.Config;
using Game.Shared.Abstract;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class MouseInputService : AbstractService
    {
        public readonly ReactiveCommand OnPress;
        public readonly ReactiveCommand OnRelease;
        public readonly ReactiveCommand<float> OnDrag;

        [Inject] private PlayerConfig _playerConfig;

        private Vector3 mouseDownPos;
        private bool _isMouseDown;
        private float _dragFactor;

        public MouseInputService()
        {
            OnPress = new ReactiveCommand();
            OnRelease = new ReactiveCommand();
            OnDrag = new ReactiveCommand<float>();
        }

        [Inject]
        private void Initalize()
        {
            Observable.EveryUpdate()
                      .Subscribe(_ => OnUpdate())
                      .AddTo(Disposer);
            
            _dragFactor = _playerConfig.HorizontalDragFactor;
            
#if UNITY_EDITOR
            _dragFactor = _dragFactor * 2f;
#endif
        }

        private void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPos = Input.mousePosition;
                _isMouseDown = true;
                OnPress.Execute();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isMouseDown = false;
                OnRelease.Execute();
            }

            if (_isMouseDown)
            {
                var drag = -(Input.mousePosition - mouseDownPos).x;
                drag *= _dragFactor;
                mouseDownPos = Input.mousePosition;
                OnDrag.Execute(drag);
            }
        }
    }
}