using Game.Player;
using Game.Shared.Abstract;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : AbstractConfig
    {
        public PlayerView PlayerPrefab;
        public float JumpForce = 5;
        public float HorizontalDragFactor = 0.05f;
        public float SpeedZ = 5;
        public float JumpDuration = 0.7f;
    }
}