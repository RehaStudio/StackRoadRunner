using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LostPanel : UIPanel
{
    [SerializeField] private Button RestartButton;

    private GameManager _GameManager;
    [Inject]
    private void Constructor(GameManager gameManager)
    {
        _GameManager = gameManager;
    }

    public override void Initialize()
    {
        base.Initialize();
        RestartButton.onClick.AddListener(Restart);
    }
    private void Restart()
    {
        _GameManager.LevelRestarted();
        Hide();
    }
    private void OnDestroy()
    {
        if (RestartButton != null)
            RestartButton.onClick.RemoveListener(Restart);
    }
}
