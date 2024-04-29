using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinPanel : UIPanel
{
    #region Fields
    [SerializeField] private Button NextLevelButton;

    private PlayerManager _PlayerManager;
    #endregion
    [Inject]
    private void Constructor(PlayerManager playerManager)
    {
        _PlayerManager = playerManager;
    }
    public override void Initialize()
    {
        base.Initialize();
        NextLevelButton.onClick.AddListener(NextLevel);
    }
    private void NextLevel()
    {
        _PlayerManager.PlayerGoStartPosition();
        Hide();  
    }
    private void OnDestroy()
    {
        if (NextLevelButton != null)
            NextLevelButton.onClick.RemoveListener(NextLevel);
    }
}
