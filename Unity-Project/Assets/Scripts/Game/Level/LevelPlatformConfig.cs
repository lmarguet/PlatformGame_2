using System.Collections.Generic;
using Game.Boost;
using UnityEngine;

namespace Game.Level
{
    public class LevelPlatformConfig
    {
        public int Id { get; private set; }
        public float PosX { get; private set; }
        public BoostType Boost { get; private set; }
        public float PosZ { get; private set; }

        public static List<LevelPlatformConfig> FromPrefab(GameObject prefab)
        {
            var list = new List<LevelPlatformConfig>();
            var level = Object.Instantiate(prefab);

            foreach (Transform platform in level.transform)
            {
                var platformData = platform.GetComponent<LevelEditorPlatform>();

                var platformConfig = new LevelPlatformConfig
                {
                    Id = platform.GetSiblingIndex(),
                    PosX = platform.position.x,
                    PosZ = platform.position.z,
                    Boost = platformData.BoostType
                };

                list.Add(platformConfig);
            }

            Object.Destroy(level);

            return list;
        }
    }
}