using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStateMachine _PlayerStateMachine;
    public void CustomInitialize()
    {
        _PlayerStateMachine = new PlayerStateMachine(this);
    }
    public void GoForward(float value)
    {
        transform.DOMoveZ(value, 4f).SetSpeedBased().SetRelative().SetEase(Ease.Linear);
    }
    public void GoRight(float value) 
    {
        transform.DOMoveX(value, 4f).SetSpeedBased().SetEase(Ease.Linear);
    }
}
