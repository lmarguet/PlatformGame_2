using UnityEngine;
using Game.Initializers;
using Zenject;

public class InitializersInstaller : Installer
{
    public override void InstallBindings()
    {
        Debug.Log("Running Intializers installer");

        Container.BindInterfacesAndSelfTo<HudInitializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<UiFlowInitializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerGameInitializer>().AsSingle().WhenInjectedInto<GameInitializer>();
        Container.BindInterfacesAndSelfTo<LevelInitializer>().AsSingle().WhenInjectedInto<GameInitializer>();
        
        Container.BindInterfacesAndSelfTo<GameInitializer>().AsSingle();
    }
}