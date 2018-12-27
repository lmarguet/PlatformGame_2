using Game.Config;
using Zenject;

namespace Game.Initializers
{
    public class HudInitializer : AbstractInitializer, IDisposableGameInitializer
    {
        [Inject] private UiConfig _uiConfig;
        [Inject] private GameStateModel _gameStateModel;

        public override void Initialize()
        {
//            var scoreView = InstantiatePrefabInRoot(_uiConfig.ScorePrefab);
//
//            var scoreViewController = new PlayerScoreViewController(scoreView).AddTo(Disposer);
//            Inject(scoreViewController);
        }
    }
}