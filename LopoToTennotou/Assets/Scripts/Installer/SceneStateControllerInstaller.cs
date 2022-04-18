using SceneController;
using UnityEngine;
using Zenject;

public class SceneStateControllerInstaller : MonoInstaller
{
    [SerializeField]
    GameObject stateController;

    public override void InstallBindings()
    {
        Container.Bind<SceneStateController>()
            .FromComponentOn(stateController)
            .AsCached();
    }
}