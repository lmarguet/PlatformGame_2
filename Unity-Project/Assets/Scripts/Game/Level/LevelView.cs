using System.Collections.Generic;
using DG.Tweening;
using Game.Boost;
using Game.Config;
using Game.Shared;
using Game.Shared.Abstract;
using UnityEngine;

namespace Game.Level
{
    public class LevelView : BaseView
    {
        private const float PlatformSizeReduceSpeed = 0.002f;
        public bool IsGameRunning { get; set; }
        public const int VisibleBuffer = 5;

        private GlobalLevelConfig _globalLevelConfig;
        private List<LevelPlatformConfig> _levelPlatformsConfig;
        private List<PlatformView> _platforms;
        private float _currentPlatformSize;

        private ObjectPool<PlatformView> _platformPool;
        private Color _newPlatformColor;

        private void Update()
        {
            if (IsGameRunning)
            {
                _currentPlatformSize -= PlatformSizeReduceSpeed;
                _currentPlatformSize = Mathf.Max(_currentPlatformSize, _globalLevelConfig.PlatformMinScale);

                foreach (var platform in _platforms)
                {
                    platform.SetSize(_currentPlatformSize);
                }
            }
        }

        public int NumPlatforms
        {
            get { return _levelPlatformsConfig.Count; }
        }

        public void SetupLevel(GlobalLevelConfig globalLevelConfig, GameObject levelConfigPrefab)
        {
            _globalLevelConfig = globalLevelConfig;
            _currentPlatformSize = _globalLevelConfig.PlatformMaxScale;
            _newPlatformColor = Color.white;

            _levelPlatformsConfig = LevelPlatformConfig.FromPrefab(levelConfigPrefab);

            _platforms = new List<PlatformView>();
            _platformPool = new ObjectPool<PlatformView>(_globalLevelConfig.PlatformPrefab);

            var totalPlatformsVisible = _globalLevelConfig.MaxVisiblePlatforms + VisibleBuffer;
            for (var i = 0; i < totalPlatformsVisible; i++)
            {
                CreatePlatform(_levelPlatformsConfig[i]);
            }

            ShowPlatformRange(0);
        }

        public void ShowPlatformRange(int start)
        {
            var startIndex = Mathf.Max(start - VisibleBuffer, 0);
            var endIndex = Mathf.Min(startIndex + _globalLevelConfig.MaxVisiblePlatforms, NumPlatforms);
            var highestIndex = 0;
            var releasedPlatforms = new List<PlatformView>();

            foreach (var platform in _platforms)
            {
                if (platform.Index < startIndex)
                {
                    releasedPlatforms.Add(platform);
                }

                highestIndex = Mathf.Max(highestIndex, platform.Index);
            }

            ReleasePlatforms(releasedPlatforms);

            CreatePlatformsRange(highestIndex, endIndex);
        }

        private void CreatePlatformsRange(int startIndex, int endIndex)
        {
            for (var i = startIndex + 1; i < endIndex; i++)
            {
                CreatePlatform(_levelPlatformsConfig[i]);
            }
        }

        private void ReleasePlatforms(List<PlatformView> releasedPlatforms)
        {
            foreach (var platform in releasedPlatforms)
            {
                _platforms.Remove(platform);

                DestroyBoostInPlatform(platform);

                _platformPool.ReleaseInstance(platform);
            }
        }

        private static void DestroyBoostInPlatform(PlatformView platform)
        {
            var boost = platform.GetComponentInChildren<BoostScript>();
            if (boost != null)
            {
                Destroy(boost.gameObject);
            }
        }

        private void CreatePlatform(LevelPlatformConfig config)
        {
            var platform = InstantiatePlatform();

            platform.transform.localPosition = new Vector3(config.PosX, 0, config.PosZ);

            platform.SetSize(_currentPlatformSize);
            platform.tag = GameTag.Platform;
            platform.Index = config.Id;
            platform.Renderer.material.color = _newPlatformColor;

            if (config.Boost != BoostType.None)
            {
                AddBoost(platform, config.Boost);
            }

            var isLastPlatform = config.Id == NumPlatforms - 1;
            if (isLastPlatform)
            {
                AddFlag(platform);
            }

            _platforms.Add(platform);
        }

        private void AddFlag(PlatformView platForm)
        {
            Instantiate(_globalLevelConfig.FlagPrefab, platForm.transform);
        }

        private void AddBoost(PlatformView platform, BoostType boostType)
        {
            var boost = Instantiate
            (
                _globalLevelConfig.GetBoostPrefab(boostType),
                platform.transform.position,
                Quaternion.identity,
                platform.transform
            );

            boost.GetComponent<BoostScript>().BoostType = boostType;
        }

        private PlatformView InstantiatePlatform()
        {
            var instance = _platformPool.GetInstance();
            instance.transform.SetParent(transform);
            return instance;
        }

        public Vector3 GetPlatformLocalPosition(int index)
        {
            return GetPlatformByIndex(index).transform.localPosition;
        }

        public Vector3 GetPlatformGlobalPosition(int index)
        {
            return GetPlatformByIndex(index).transform.position;
        }

        private PlatformView GetPlatformByIndex(int index)
        {
            foreach (var platform in _platforms)
            {
                if (platform.Index == index)
                {
                    return platform;
                }
            }

            return null;
        }

        public void BoostPlatformsSize()
        {
            _currentPlatformSize = _globalLevelConfig.PlatformMaxScale;

            _newPlatformColor = _globalLevelConfig.RandomBoostColor(_newPlatformColor);

            foreach (var platform in _platforms)
            {
                platform.SetSize(_currentPlatformSize);
                var scale = platform.transform.localScale;
                scale *= 1.01f;
                platform.transform.DOPunchScale(scale, 0.25f);
                platform.Renderer.material.DOColor(_newPlatformColor, 0.25f);
            }
        }

        public void BoostAlighPath(int currentPlatform)
        {
            var numAlign = _globalLevelConfig.AlignBoostNumPlatforms;
            var startIndex = currentPlatform + 1;
            var maxIndex = NumPlatforms - 1;

            var lastZ = GetPlatformByIndex(currentPlatform).transform.localPosition.z;

            for (var i = startIndex; i < maxIndex; i++)
            {
                var platform = GetPlatformByIndex(i);
                var platformZ = platform.transform.localPosition.z;

                if (platformZ > lastZ)
                {
                    platform.transform
                            .DOLocalMoveX(0, 0.2f)
                            .SetEase(Ease.OutBack);

                    lastZ = platformZ;
                    numAlign--;
                }

                if (numAlign == 0)
                {
                    break;
                }
            }
        }

        public void ResetPlatformScale()
        {
            _currentPlatformSize = _globalLevelConfig.PlatformMaxScale;

            foreach (var platform in _platforms)
            {
                platform.SetSize(_currentPlatformSize);
            }
        }
    }
}