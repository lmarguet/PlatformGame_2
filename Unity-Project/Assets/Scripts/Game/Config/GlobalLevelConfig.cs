using System;
using Game.Boost;
using Game.Level;
using Game.Shared.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "GlobalLevelConfig", menuName = "Config/GlobalLevelConfig")]
    public class GlobalLevelConfig : AbstractConfig
    {
        public LevelView TestLevelPrefab;
        public PlatformView PlatformPrefab;
        public GameObject FlagPrefab;

        [Header("Boost prefabs")] [SerializeField]
        public GameObject BoostResizePrefab;

        public GameObject BoostSlowMoPrefab;
        public GameObject BoostAlignPath;

        public float PlatformSpacing = 3.2f;
        public float PlatformMaxScale = 1;
        public float PlatformMinScale = 1;
        public int MaxVisiblePlatforms = 10;

        public GameObject GetBoostPrefab(BoostType boostType)
        {
            switch (boostType)
            {
                case BoostType.PlatformSizeReset:
                    return BoostResizePrefab;
                case BoostType.SlowMotion:
                    return BoostSlowMoPrefab;
                case BoostType.AlignPath:
                    return BoostAlignPath;
                default:
                    throw new ArgumentOutOfRangeException("boostType", boostType, null);
            }
        }

        [Header("Boost params")] [SerializeField]
        public Color[] PlatformResizeColors;

        public Color RandomBoostColor(Color materialColor)
        {
            while (true)
            {
                var color = GetRandomBoostColor();
                if (color.Equals(materialColor))
                {
                    continue;
                }

                return color;
            }
        }

        private Color GetRandomBoostColor()
        {
            var index = Random.Range(0, PlatformResizeColors.Length);
            return PlatformResizeColors[index];
        }

        public float BoostSlowMoJumpDuration = 1.5f;
        public int MaxSlowmotionHits = 4;
        public int AlignBoostNumPlatforms = 5;
    }
}