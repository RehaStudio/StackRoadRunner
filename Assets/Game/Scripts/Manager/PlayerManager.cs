using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    private StackManager _StackManager;

    public Player Player;

    [Inject]
    public void Constructor(StackManager stackManager)
    {
        _StackManager = stackManager;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _StackManager._OnStackPlaced += OnStackPlaced;
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
    }
}
