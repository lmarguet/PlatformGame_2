using DG.Tweening;
using Game.Shared.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UiFlow
{
    public class StartScreenView : BaseView, IPointerDownHandler
    {
        [SerializeField] private RectTransform _instructionsLabel;
        [SerializeField] private RectTransform _handIcon;

        private Vector2 _handInitPosition;
        private Vector2 _instructionsInitPosition;

        public readonly ReactiveCommand OnPress = new ReactiveCommand();
        private Sequence _handSequence;

        private void Awake()
        {
            _handInitPosition = _handIcon.anchoredPosition;
            _instructionsInitPosition = _instructionsLabel.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPress.Execute();
        }

        private void OnEnable()
        {
            PlayAnimations();
        }

        private void OnDisable()
        {
            StopAnimations();
        }

        private void PlayAnimations()
        {
            _handIcon.anchoredPosition = _handInitPosition;
            _handSequence = DOTween.Sequence()
                                   .Append(_handIcon.DOAnchorPosX(-_handInitPosition.x, 0.8f))
                                   .Append(_handIcon.DOAnchorPosX(_handInitPosition.x, 0.8f))
                                   .SetLoops(-1);

            _instructionsLabel.anchoredPosition = _instructionsInitPosition;
            _instructionsLabel.DOAnchorPosX(955, 0.3f)
                              .SetEase(Ease.OutBack)
                              .SetDelay(0.1f)
                              .From();
        }

        private void StopAnimations()
        {
            if (_handSequence != null)
            {
                _handSequence.Kill();
            }

            _instructionsLabel.DOKill();
        }
    }
}