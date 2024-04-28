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
        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.BindMemoryPool<Stack, Stack.Pool>().FromComponentInNewPrefab(Stack);
    }
}
