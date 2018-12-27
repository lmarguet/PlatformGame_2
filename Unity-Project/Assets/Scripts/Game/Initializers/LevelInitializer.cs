using Game.Config;
using Game.Level;
using Game.Score;
using UniRx;
using Zenject;

namespace Game.Initializers
{
    public class LevelInitializer: AbstractInitializer, IDisposableGameInitializer
    {
        [Inject] private GlobalLevelConfig _globalLevelsConfig;
        [Inject] private ScoreModel _scoreModel;
        
        public override void Initialize()
        {
            _scoreModel.Reset();
            
            var levelView = InstantiatePrefabInRoot(_globalLevelsConfig.TestLevelPrefab);
            var levelController = new LevelController(levelView).AddTo(Disposer);
            Inject(levelController);

        }
    }
}