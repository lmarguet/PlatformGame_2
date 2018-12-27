using Game.Score;
using Zenject;

namespace Game.Installers.Factories
{
    public class ScoreModelFactory : IFactory<ScoreModel>
    {
        public ScoreModel Create()
        {
            return new ScoreModel();
        }
    }
}