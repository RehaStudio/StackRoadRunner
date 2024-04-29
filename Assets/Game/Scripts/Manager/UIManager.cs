using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    private GameManager _GameManager;
    public List<UIPanel> _Panels;
    [Inject]
    private void Constructor(GameManager gameManager)
    {
        _GameManager = gameManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _GameManager.OnLevelCompleted += OnLevelCompleted;
        _GameManager.OnLevelFailed += OnLevelFailed;
        _Panels.ForEach(e => e.Initialize());
        ShowPanel<MainMenuPanel>();
    }
    public T GetPanel<T>() where T : UIPanel
    { 
        foreach (UIPanel panel in _Panels) 
        {
            if (panel is T)
                return (T)panel;
        }
        throw new System.NotImplementedException();
    }
    private void ShowPanel<T>() where T : UIPanel
    {
        GetPanel<T>().Show();
    }
    private void OnLevelFailed()
    {
        ShowPanel<LostPanel>();
    }

    private void OnLevelCompleted()
    {
   
    }
}
