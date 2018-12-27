using Game.Config;
using Zenject;

namespace Game.Installers.Factories
{
    public class GameModelFactory : IFactory<GameStateModel>
    {
        [Inject] private PlayerConfig _playerConfig;

        public GameStateModel Create()
        {
            return new GameStateModel();
        }
    }
}