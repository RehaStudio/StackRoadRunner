using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinPanel : UIPanel
{
    [SerializeField] private Button NextLevelButton;

    private GameManager _GameManager;
    [Inject]
    private void Constructor(GameManager gameManager)
    {
        _GameManager = gameManager;
    }
    public override void Initialize()
    {
        base.Initialize();
        NextLevelButton.onClick.AddListener(NextLevel);
    }
    private void NextLevel()
    { 
        
    }
    private void OnDestroy()
    {
        if (NextLevelButton != null)
            NextLevelButton.onClick.RemoveListener(NextLevel);
    }
}
