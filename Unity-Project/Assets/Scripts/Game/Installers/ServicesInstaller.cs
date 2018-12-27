using Game.Services;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class ServicesInstaller : Installer
    {
        public override void InstallBindings()
        {
            Debug.Log("Running services installer");
            
            Container.Bind<MouseInputService>().AsSingle().NonLazy();
            Container.Bind<BoostService>().AsSingle();
        }
    }
}