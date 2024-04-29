using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    private GameManager _GameManager;
    private PlayerManager _PlayerManager;
    #region Fields
    [SerializeField] private List<Level> _Levels = new List<Level>();
    [SerializeField] private Transform _FinishObject;
    [SerializeField] private Transform _StartObject;

    public Level _CurrentLevel { get; private set; }
    public int CurrentLevelIndex { get; private set; }
    #endregion
    [Inject]
    private void Constructor(GameManager gameManager,PlayerManager playerManager)
    {
        _GameManager = gameManager;
        _PlayerManager = playerManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _GameManager.OnLevelCompleted += OnLevelCompleted;
        _GameManager.OnLevelStarted += OnLevelStarted;
        _GameManager.OnLevelRestarted += _OnLevelRestarted;
        CreateLevel();
        UpdateStartFinishPosition();
    }
    private void OnLevelCompleted()
    {
        CurrentLevelIndex++;
        CreateLevel();
    }
    private void _OnLevelRestarted()
    {
        CreateLevel();
        UpdateStartFinishPosition();
    }

    private void OnLevelStarted()
    {
        UpdateStartFinishPosition();
    }
    private void CreateLevel()
    {
        _CurrentLevel?.DespawnLevelCollectibles();
        _CurrentLevel = _Levels[CurrentLevelIndex % _Levels.Count];
    }
    private void UpdateStartFinishPosition()
    {
        _FinishObject.transform.position = _FinishObject.transform.position.SetZ(_PlayerManager.Player.transform.position.z + (_CurrentLevel.StackCount * Constants.StackStepSize));
        _StartObject.transform.position = _StartObject.transform.position.SetZ((_PlayerManager.Player.transform.position.z + Constants.StackStepSize));
        _CurrentLevel.CreateCollectibles(_PlayerManager.Player.transform.position.z);
    }
    private void OnDestroy()
    {
        if (_GameManager != null) 
        {
            _GameManager.OnLevelCompleted -= OnLevelCompleted;
        }
    }
}
