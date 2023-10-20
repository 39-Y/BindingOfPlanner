using Action;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ActionService>().To<ActionService>().AsSingle();
    }
}