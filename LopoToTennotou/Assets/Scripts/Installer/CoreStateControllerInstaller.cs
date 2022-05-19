using GameCore;
using UnityEngine;
using Zenject;

public class CoreStateControllerInstaller : MonoInstaller
{
    [SerializeField]
    GameObject gameObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CoreStateController>()
            .FromComponentOn(gameObj)
            .AsSingle();
    }
}