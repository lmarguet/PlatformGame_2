using System.Collections.Generic;
using System.Linq;
using Game.Config;
using Game.Level;
using UnityEngine;

namespace Game.Test
{
    public class LevelGeneratorRoot : MonoBehaviour
    {
        [SerializeField] private GlobalLevelConfig _levelConfig;
        [SerializeField] private Transform _container;
        [SerializeField] private int _numPlatforms;
        [SerializeField] private GameObject _debugPrefab;
        [SerializeField] private Transform _outputContainer;

        private void Start()
        {
            CopyToNewLevel();

//            GenerateInitPlatforms();
        }

        private void GenerateInitPlatforms()
        {
            for (var i = 0; i < _numPlatforms; i++)
            {
                var platform = Instantiate(_debugPrefab, _outputContainer);
                platform.transform.Translate(0, 0, _levelConfig.PlatformSpacing * i);
            }
        }

        private void CopyToNewLevel()
        {
            var list = new List<LevelEditorPlatform>();

            foreach (Transform original in _container)
            {
                var platform = Instantiate(_debugPrefab, original.position, Quaternion.identity, _outputContainer);
                var sortId = ((int) (platform.transform.position.z / _levelConfig.PlatformSpacing));
                var platformScript = platform.GetComponent<LevelEditorPlatform>();
                platformScript.Id = sortId;
                list.Add(platformScript);
            }

            list = list.OrderBy(x => x.Id).ToList();

            for (var i = 0; i < list.Count; i++)
            {
                list[i].Id = i;
                list[i].transform.SetSiblingIndex(i);
            }
        }
    }
}