using GameCore;
using Zenject;

public class ConverterToVecterLogicInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<ConverterToVecterLogic>()
            .AsSingle();
    }
}