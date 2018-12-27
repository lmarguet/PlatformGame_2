using Game.HUD;
using Game.Shared.Abstract;
using Game.UiFlow;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "UiConfig", menuName = "Config/UiConfig")]
    public class UiConfig:AbstractConfig
    {
        public ScoreView ScorePrefab;
        public StartScreenView StartScreenPrefab;
        public GameOverScreenView GameOverScreenPrefab;
        public LevelCompleteScreenView LevelCompleteScreenPrefab;
    }
}