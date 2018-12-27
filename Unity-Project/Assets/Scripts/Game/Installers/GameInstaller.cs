using Game.Installers.Factories;
using Game.Score;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private CameraView _camera;

        public override void InstallBindings()
        {
            Debug.Log("Running game installer");

            Container.Bind<GameStateModel>().FromFactory<GameModelFactory>().AsSingle();
            Container.Bind<ScoreModel>().FromFactory<ScoreModelFactory>().AsSingle();
            Container.Bind<GameStateManager>().AsSingle();
            Container.Bind<EffectsManager>().AsSingle().NonLazy();

            Container.BindInstance(_camera);

            Container.Install<ServicesInstaller>();
            Container.Install<InitializersInstaller>();
        }
    }
}