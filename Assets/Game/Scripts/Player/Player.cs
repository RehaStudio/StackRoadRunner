using DG.Tweening;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStateMachine _PlayerStateMachine;
    public Animator _Animator { get; private set; }
    private Tween _ForwardMoveTween;
    public void Initialize()
    {
        _Animator = GetComponent<Animator>();
        _PlayerStateMachine = new PlayerStateMachine(this);
    }
    public void GoForward(float value,Action onCompleted = null)
    {
        if (_ForwardMoveTween != null)
            _ForwardMoveTween.Kill();
        _ForwardMoveTween = transform.DOMoveZ(value, 4f).SetSpeedBased().SetRelative().SetEase(Ease.Linear);
        _ForwardMoveTween.OnComplete(() => onCompleted?.Invoke());
    }
    public void GoRight(float value) 
    {
        transform.DOMoveX(value, 4f).SetSpeedBased().SetEase(Ease.Linear);
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void Dance()
    {
        _PlayerStateMachine.ChangeStateTo<PlayerDanceState>();
    }
    public void Run()
    {
        _PlayerStateMachine.ChangeStateTo<PlayerRunState>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<IInteract<Player>>(out IInteract<Player> interactObject))
            interactObject.Interact(this);
    }
}
