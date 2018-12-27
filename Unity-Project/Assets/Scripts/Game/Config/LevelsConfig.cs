using Game.Shared.Abstract;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Config/LevelsConfig")]
    public class LevelsConfig: AbstractConfig
    {
        public GameObject[] Levels;

    }
}