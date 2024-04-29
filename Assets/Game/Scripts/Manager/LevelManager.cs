using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{

    #region Fields
    [SerializeField] private List<Level> _Levels = new List<Level>();

    private StackManager _StackManager;

    private Level _CurrentLevel;
    private int _PlacedStackCount;
    #endregion
    [Inject]
    public void Constructor(StackManager stackManager)
    {
        _StackManager = stackManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _StackManager._OnStackPlaced += OnStackPlaced;
        CreateLevel();
    }

    private void OnStackPlaced(Stack stack)
    {
        _PlacedStackCount++;
        if (_PlacedStackCount == _CurrentLevel.StackCount)
            Debug.Log("GameFinished");
    }

    private void CreateLevel()
    {
        _CurrentLevel = _Levels[0];
    }
    #region Unity_Function
    private void OnDestroy()
    {
        if (_StackManager != null)
            _StackManager._OnStackPlaced -= OnStackPlaced;
    }
    #endregion
}
