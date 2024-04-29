using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StackManager :MonoBehaviour
{
    #region Events
    public event Action<Stack> _OnStackPlaced;
    #endregion
    #region Fields
    [SerializeField] private Transform _StackGroupParent;
    [SerializeField] private Color[] _StackColors;

    private InputManager _InputManager;
    private SoundManager _SoundManager;
    private GameManager _GameManager;
    private Stack.Pool _StackPool;
    private BreakStack.Pool _BreakStackPool;

    private List<Stack> _Stacks = new List<Stack>();

    private int _StackCount;
    private float _MoveDirection = 1;
    private float _TresholdPerfectMatch = .2f;

    private int ComboPerfectMatch = 0;
    #endregion

    #region Properties
    private Stack _CurrentStack => _Stacks.Last();
    private Stack _PreviousStack => _Stacks.Count > 1 ? _Stacks[_Stacks.Count - 2] : null;
    private float _CurrentStackStartSize => _PreviousStack != null ? _PreviousStack.GetSize() : Constants.StackStartSize;
    private float _CenterPosition => _PreviousStack != null ? _PreviousStack.GetLocalPosition().x : 0;
    #endregion

    [Inject]
    private void Constructor(InputManager inputManager,SoundManager soundManager,GameManager gameManager)
    {
        _InputManager = inputManager;
        _SoundManager = soundManager;
        _GameManager = gameManager;
        CustomInitialize();
    }
    [Inject]
    private void ConstructorPool(Stack.Pool stackPool, BreakStack.Pool breakStackPool)
    {
        _StackPool = stackPool;
        _BreakStackPool = breakStackPool;
    }
    private void CustomInitialize()
    {
        _GameManager.OnLevelStarted += OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        ClearStacks();
        CreateStack();
        _InputManager.OnMouseButtonDowned += OnMouseButtonDowned;
    }
    private void ClearStacks()
    {
        _StackCount = 0;
        _Stacks.ForEach(e => _StackPool.Despawn(e));
        _Stacks.Clear();
    }
    private void OnMouseButtonDowned()
    {
        _CurrentStack.StopMove();
        if (!CanStackPlace())
            return;
        if (IsCompleted())
            Completed();
        else
            CreateStack();
    }
    private void CreateStack()
    {
        _Stacks.Add(_StackPool.Spawn());

        _CurrentStack.transform.SetParent(_StackGroupParent);
        _CurrentStack.SetLocalPosition(_StackCount * Vector3.forward * Constants.StackStepSize + Vector3.right * 4f * _MoveDirection);
        _CurrentStack.SetColor(_StackColors[_StackCount % _StackColors.Length]);
        _CurrentStack.SetSize(_CurrentStackStartSize);
        _CurrentStack.MoveHorizontal(_MoveDirection * (-4f));

        _MoveDirection *= -1;
        _StackCount++;
        RemoveNotSeenStack();
    }

    private bool CanStackPlace()
    {
        float centerPosition = _CenterPosition;
        float distance = _CurrentStack.GetLocalPosition().x - centerPosition;

        if (Mathf.Abs(distance) > _CurrentStack.GetSize())
        {
            FallBreakStack(_CurrentStack.GetSize());
            Fail();
            return false;
        }
        PlaceStack(distance);
        return true;
    }
    private void PlaceStack(float distance)
    {
        float magnitude = Mathf.Abs(distance);
        if (magnitude <= _TresholdPerfectMatch)
            PerfectMatch();
        else
        {
            _CurrentStack.SetSize(_CurrentStack.GetSize() - magnitude);
            _CurrentStack.SetLocalPosition(_CurrentStack.GetLocalPosition() + distance * Vector3.left / 2);
            FallBreakStack(distance);
            ComboPerfectMatch = 0;
        }
        _OnStackPlaced?.Invoke(_CurrentStack);
    }
    private void PerfectMatch()
    {
        _CurrentStack.SetSize(_CurrentStackStartSize);
        _CurrentStack.SetLocalPosition(_CurrentStack.GetLocalPosition().SetX(_CenterPosition));
        _SoundManager.PlayStackPerfectMatch(ComboPerfectMatch);
        ComboPerfectMatch++;
    }
    private void FallBreakStack(float distance)
    {
        BreakStack breakStack = _BreakStackPool.Spawn();
        breakStack.transform.SetParent(_StackGroupParent);
        breakStack.SetColor(_CurrentStack.GetColor());
        breakStack.SetSize(Mathf.Abs(distance));
        if (_CurrentStack.GetSize() != distance)
            breakStack.SetLocalPosition(_CurrentStack.GetLocalPosition() + Mathf.Sign(distance) * _CurrentStack.GetSize() * Vector3.right / 2 - distance * Vector3.left / 2);
        else
            breakStack.SetLocalPosition(_CurrentStack.GetLocalPosition());
        breakStack.Fall();
    }


    private void RemoveNotSeenStack()
    {
        if (_Stacks.Count > 4)
        {
            _StackPool.Despawn(_Stacks.First());
            _Stacks.RemoveAt(0);
        }
    }
    private bool IsCompleted()
    {
        if (_StackCount == _GameManager.LevelStackCount)
            return true;
        return false;
    }
    private void Completed()
    {
        _GameManager.LevelCompleted();
        _InputManager.OnMouseButtonDowned -= OnMouseButtonDowned;
    }

    private void Fail()
    {
        _StackPool.Despawn(_CurrentStack);
        _Stacks.Remove(_CurrentStack);
        _GameManager.LevelFailed();
        _InputManager.OnMouseButtonDowned -= OnMouseButtonDowned;
    }
    #region Unity_Functions
    private void OnDestroy()
    {
        if (_InputManager != null)
            _InputManager.OnMouseButtonDowned -= OnMouseButtonDowned;
        if(_GameManager != null)
            _GameManager.OnLevelStarted -= OnLevelStarted;
    }
    #endregion
}
