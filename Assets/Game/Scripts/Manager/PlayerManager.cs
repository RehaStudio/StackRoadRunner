using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    private StackManager _StackManager;
    private GameManager _GameManager;

    public Player Player;

    [Inject]
    public void Constructor(StackManager stackManager,GameManager gameManager)
    {
        _StackManager = stackManager;
        _GameManager = gameManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        Player.Initialize();
        _StackManager._OnStackPlaced += OnStackPlaced;
        _GameManager.OnLevelCompleted += OnLevelCompleted;
        _GameManager.OnLevelStarted += OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        Player.SetPosition(Vector3.zero);
    }

    private void OnLevelCompleted()
    {
        Player.GoForward(6f, Player.Dance);
    }

    private void OnStackPlaced(Stack stack)
    {
        Player.GoRight(stack.GetLocalPosition().x);
        Player.GoForward(Constants.StackStepSize);
    }
    private void OnDestroy()
    {
        if (_StackManager != null)
            _StackManager._OnStackPlaced -= OnStackPlaced;
        if (_GameManager != null)
        { 
            _GameManager.OnLevelCompleted -= OnLevelCompleted;
            _GameManager.OnLevelStarted -= OnLevelStarted;
        }
    }
}
