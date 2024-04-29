using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Zenject;

public class MainMenuPanel : UIPanel
{
    #region Fields
    private GameManager _GameManager;
    private InputManager _InputManager;
    #endregion
    [Inject]
    private void Constructor(GameManager gameManager, InputManager inputManager)
    {
        _GameManager = gameManager;
        _InputManager = inputManager;
    }
    public override void Initialize()
    {
        base.Initialize();
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _InputManager.OnMouseButtonDowned += _OnMouseButtonDowned;
    }

    private void _OnMouseButtonDowned()
    {
        _GameManager.LevelStarted();
        Hide();
        _InputManager.OnMouseButtonDowned -= _OnMouseButtonDowned;
    }
    #region Unity_Functions
    private void OnDestroy()
    {
        if (_InputManager != null)
            _InputManager.OnMouseButtonDowned -= _OnMouseButtonDowned;
    }
    #endregion
}
