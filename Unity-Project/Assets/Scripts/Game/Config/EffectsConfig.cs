using Game.Shared.Abstract;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "EffectsConfig", menuName = "Config/EffectsConfig")]
    public class EffectsConfig: AbstractConfig
    {
        public GameObject FireWorksPrefab;
        public GameObject SplashPrefab;
        public GameObject BoostActivatePrefab;
    }
}