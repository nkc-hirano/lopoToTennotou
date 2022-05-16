using GameCore;
using UnityEngine;
using Zenject;

public class CoreStateControllerInstaller : MonoInstaller
{
    [SerializeField]
    GameObject gameObject;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CoreStateController>()
            .FromComponentOn(gameObject)
            .AsSingle();
    }
}