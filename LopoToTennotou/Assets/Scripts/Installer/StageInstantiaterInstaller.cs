using GameCore;
using UnityEngine;
using Zenject;

public class StageInstantiaterInstaller : MonoInstaller
{
    [SerializeField]
    GameObject componentObject;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StageInstantiater>()
            .FromComponentOn(componentObject)
            .AsSingle();
    }
}