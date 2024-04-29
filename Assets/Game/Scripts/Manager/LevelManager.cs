using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{

    #region Fields
    [SerializeField] private List<Level> _Levels = new List<Level>();

    public Level _CurrentLevel { get; private set; }
    #endregion
    [Inject]
    private void CustomInitialize()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        _CurrentLevel = _Levels[0];
    }
}
