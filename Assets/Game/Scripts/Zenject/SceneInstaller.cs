using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    #region Fields
    public Stack Stack;
    public BreakStack BreakStack;
    #endregion
    public override void InstallBindings()
    {
        Container.BindMemoryPool<Stack, Stack.Pool>().FromComponentInNewPrefab(Stack);
        Container.BindMemoryPool<BreakStack, BreakStack.Pool>().FromComponentInNewPrefab(BreakStack);
        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SoundManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<StackManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<VFXManager>().FromComponentInHierarchy().AsSingle();
    }
}
