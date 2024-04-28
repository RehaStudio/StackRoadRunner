using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    #region Fields
    public Stack Stack;
    #endregion
    public override void InstallBindings()
    {
        Container.BindMemoryPool<Stack, Stack.Pool>().FromComponentInNewPrefab(Stack);
        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<StackManager>().FromComponentInHierarchy().AsSingle();
    }
}
