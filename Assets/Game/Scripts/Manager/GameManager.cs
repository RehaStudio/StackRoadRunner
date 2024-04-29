using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager: MonoBehaviour
{
    #region Fields
    private LevelManager _LevelManager;
    #endregion
    public int LevelStackCount => _LevelManager._CurrentLevel.StackCount;
    #region Events
    public event Action OnLevelStarted;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    #endregion
    [Inject]
    private void Constructor(LevelManager levelManager)
    {
        _LevelManager = levelManager;
    }
    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    public void LevelStarted()
    {
        OnLevelStarted?.Invoke();
    }
}