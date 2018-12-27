using Game.Config;
using Game.UiFlow;
using UniRx;
using Zenject;

namespace Game.Initializers
{
    public class UiFlowInitializer : AbstractInitializer
    {
        [Inject] private UiConfig _uiConfig;
        [Inject] private GameStateModel _gameStateModel;

        public override void Initialize()
        {
            var startScreenView = InstantiatePrefabInRoot(_uiConfig.StartScreenPrefab);
            var statsController = new StartScreenController(startScreenView).AddTo(Disposer);
            Inject(statsController);

            var gameOverScreenView = InstantiatePrefabInRoot(_uiConfig.GameOverScreenPrefab);
            var gameOverScreenController = new GameOverScreenController(gameOverScreenView).AddTo(Disposer);
            Inject(gameOverScreenController);

            var levelCompleteScreenView = InstantiatePrefabInRoot(_uiConfig.LevelCompleteScreenPrefab);
            var levelCompleteScreenController = new LevelCompleteScreenController(levelCompleteScreenView).AddTo(Disposer);
            Inject(levelCompleteScreenController);
        }
    }
}