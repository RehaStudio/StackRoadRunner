using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StackManager :MonoBehaviour
{
    #region Fields
    private InputManager _InputManager;
    private Stack.Pool _StackPool;

    private List<Stack> _Stacks = new List<Stack>();

    private int _StackCount;
    private float _MoveDirection = 1;

    [SerializeField] private Transform _StackGroupParent;
    [SerializeField] private Color[] _StackColors;
    #endregion

    #region Properties
    private Stack _CurrentStack => _Stacks.Last();
    private Stack _PreviousStack => _Stacks.Count > 1 ? _Stacks[_Stacks.Count - 2] : null;

    private float _CurrentStackStartSize => _PreviousStack != null ? _PreviousStack.GetSize() : Constants.StackStartSize;
    #endregion

    [Inject]
    private void Constructor(InputManager inputManager,Stack.Pool stackPool)
    {
        _InputManager = inputManager;
        _StackPool = stackPool;
        CustomInitialize();
    }
    private void CustomInitialize()
    {
        _InputManager.OnMouseButtonDowned += OnMouseButtonDowned;
        CreateStack();
    }

    private void OnMouseButtonDowned()
    {
        _CurrentStack.StopMove();
        CheckStackPlacement();
        CreateStack();
    }
    private void CheckStackPlacement()
    {
        float centerPosition = _PreviousStack != null ? _PreviousStack.GetLocalPosition().x : 0;
        float distance = _CurrentStack.GetLocalPosition().x - centerPosition;
        float magnitude = Mathf.Abs(distance);

        if (magnitude > _CurrentStack.GetSize())
        {
            Fail();
            return;
        }
        _CurrentStack.SetSize(_CurrentStack.GetSize() - magnitude);
        _CurrentStack.SetLocalPosition(_CurrentStack.GetLocalPosition() + distance * Vector3.left / 2);
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
    private void RemoveNotSeenStack()
    {
        if (_Stacks.Count > 3)
        {
            _StackPool.Despawn(_Stacks.First());
            _Stacks.RemoveAt(0);
        }
    }

    private void Fail()
    {
        Debug.Log("Level Failed");
    }
    #region Unity_Functions
    private void OnDestroy()
    {
        if (_InputManager != null)
            _InputManager.OnMouseButtonDowned -= OnMouseButtonDowned;
    }
    #endregion
}
