using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StackManager :MonoBehaviour
{
    #region Fields
    private InputManager _InputManager;
    private Stack.Pool _StackPool;
    private Stack _CurrentStack;
    private int _StackCount;
    [SerializeField] private Transform _StackGroupParent;
    [SerializeField] private Color[] _StackColors;
    #endregion
    [Inject]
    private void Constructor(InputManager inputManager,Stack.Pool stackPool)
    {
        _InputManager = inputManager;
        _StackPool = stackPool;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _InputManager.OnMouseButtonDowned += OnMouseButtonDowned;
        CreateStack();
    }

    private void OnMouseButtonDowned()
    {
        CreateStack();
    }
    private void CreateStack()
    {
        _CurrentStack = _StackPool.Spawn();
        _CurrentStack.transform.SetParent(_StackGroupParent);
        _CurrentStack.SetLocalPosition(_StackCount * Vector3.forward * 3f);
        _CurrentStack.SetColor(_StackColors[_StackCount % _StackColors.Length]);
        _StackCount++;
    }
    #region Unity_Functions
    private void OnDestroy()
    {
        if (_InputManager != null)
            _InputManager.OnMouseButtonDowned -= OnMouseButtonDowned;
    }
    #endregion
}
