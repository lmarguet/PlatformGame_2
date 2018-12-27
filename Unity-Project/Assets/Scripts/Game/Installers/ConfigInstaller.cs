using UnityEngine;
using Game.Config;
using Zenject;

[CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
{
    public PlayerConfig PlayerConfig;
    public UiConfig UiConfig;
    public MiscConfig MiscConfig;
    public GlobalLevelConfig GlobalLevelsConfig;
    public EffectsConfig EffectsConfig;
    public LevelsConfig LevelsConfig;

    public override void InstallBindings()
    {
        Debug.Log("Running config installer");
        Container.BindInstance(PlayerConfig);
        Container.BindInstance(UiConfig);
        Container.BindInstance(MiscConfig);
        Container.BindInstance(GlobalLevelsConfig);
        Container.BindInstance(EffectsConfig);
        Container.BindInstance(LevelsConfig);
    }
}