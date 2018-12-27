using DG.Tweening;
using Game.Shared.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UiFlow
{
    public class GameOverScreenView : BaseView
    {
        public readonly ReactiveCommand OnContinue = new ReactiveCommand();
        public readonly ReactiveCommand OnRetry = new ReactiveCommand();

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private RectTransform _gameOverLabel;

        private Vector2 _gameOverInitPosition;

        private void Awake()
        {
            _gameOverInitPosition = _gameOverLabel.anchoredPosition;
        }

        private void Start()
        {
            OnContinue.BindTo(_continueButton);
            OnRetry.BindTo(_retryButton);
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
            _gameOverLabel.anchoredPosition = _gameOverInitPosition;
            _gameOverLabel.DOAnchorPosX(955, 0.3f)
                          .SetEase(Ease.OutBack)
                          .SetDelay(0.1f)
                          .From();
            
            AnimateButton(_continueButton, 0.5f);
            AnimateButton(_retryButton, 0.6f);
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
            _gameOverLabel.DOKill();
        }
    }
}