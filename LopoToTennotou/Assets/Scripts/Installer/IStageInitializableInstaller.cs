using GameCore;
using UnityEngine;
using Zenject;

public class IStageInitializableInstaller : MonoInstaller
{
    [SerializeField]
    GameObject stageInitializableComponentObject;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StageDataController>()
            .FromComponentOn(stageInitializableComponentObject)
            .AsSingle();
    }
}