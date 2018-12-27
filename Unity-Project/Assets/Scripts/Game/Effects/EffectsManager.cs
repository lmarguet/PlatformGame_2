using System;
using Game.Config;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game
{
    public class EffectsManager
    {
        private static EffectsManager _instance;

        [Inject]
        public EffectsConfig EffectsConfig { get; private set; }

        [Inject]
        private void OnInject()
        {
            _instance = this;
        }

        public static void PlayEffect(Effects effectType, Vector3 position, Transform parent = null)
        {
            GameObject prefab;

            switch (effectType)
            {
                case Effects.Fireworks:
                    prefab = _instance.EffectsConfig.FireWorksPrefab;
                    break;
                case Effects.Splash:
                    prefab = _instance.EffectsConfig.SplashPrefab;
                    break;
                case Effects.BoostPickup:
                    prefab = _instance.EffectsConfig.BoostActivatePrefab;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("effectType", effectType, null);
            }


            PlayEffectAt(prefab, position, parent);
        }

        private static void PlayEffectAt(GameObject prefab, Vector3 position, Transform parent = null)
        {
            var effect = Object.Instantiate(prefab, position, Quaternion.identity);
            if (parent != null)
            {
                effect.transform.SetParent(parent);
            }
        }
    }
}