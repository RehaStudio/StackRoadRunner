using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StackManager :MonoBehaviour
{
    #region Fields
    private InputManager _InputManager;
    #endregion
    [Inject]
    private void Constructor(InputManager inputManager)
    {
        _InputManager = inputManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _InputManager.OnMouseButtonDowned += OnMouseButtonDowned;
    }

    private void OnMouseButtonDowned()
    {
       
    }
    #region Unity_Functions
    private void OnDestroy()
    {
        if (_InputManager != null)
            _InputManager.OnMouseButtonDowned -= OnMouseButtonDowned;
    }
    #endregion
}
