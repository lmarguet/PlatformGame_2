using DG.Tweening;
using Game.Shared.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UiFlow
{
    public class LevelCompleteScreenView : BaseView
    {
        public readonly ReactiveCommand OnClose = new ReactiveCommand();

        [SerializeField] private Button _closeButton;
        [SerializeField] private RectTransform _levelCompleteLabel;

        private Vector2 _levelCompleteInitPosition;

        private void Awake()
        {
            _levelCompleteInitPosition = _levelCompleteLabel.anchoredPosition;
        }

        private void Start()
        {
            OnClose.BindTo(_closeButton);
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
            _levelCompleteLabel.anchoredPosition = _levelCompleteInitPosition;
            _levelCompleteLabel.DOAnchorPosX(955, 0.3f)
                          .SetEase(Ease.OutBack)
                          .SetDelay(0.1f)
                          .From();

            AnimateButton(_closeButton, 0.5f);
        }

        private static void AnimateButton(Button button, float delay)
        {
            button.transform.localScale = Vector3.one;

            button.transform.DOPunchScale(Vector3.one, 0.2f, 5)
                  .SetDelay(delay)
                  .OnStart(() => button.gameObject.SetActive(true));

            button.gameObject.SetActive(false);
        }

        private void StopAnimations()
        {
            _levelCompleteLabel.DOKill();
        }
    }
}