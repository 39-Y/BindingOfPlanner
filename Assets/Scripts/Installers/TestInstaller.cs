using Action;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    [SerializeField] private Canvas canvas;
    public override void InstallBindings()
    {
        Container.Bind<ActionService>().To<ActionService>().AsSingle();
        Container.Bind<Canvas>().FromInstance(canvas).AsSingle();
    }
    
}