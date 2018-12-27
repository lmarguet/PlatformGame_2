using Game.Shared.Abstract;
using TMPro;
using UnityEngine;

namespace Game.HUD
{
    public class ScoreView : BaseView
    {

        [SerializeField] private TextMeshProUGUI _scoreLabel;
        
        private void Awake()
        {
            _scoreLabel.text = "0";
        }

        public void SetScore(int score)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}