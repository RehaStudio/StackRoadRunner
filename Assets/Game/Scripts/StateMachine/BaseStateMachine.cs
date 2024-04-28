using System.Collections.Generic;

public class BaseStateMachine<T>
{
    protected List<BaseState<T>> _States = new List<BaseState<T>>();

    protected BaseState<T> _CurrentState;
    protected BaseState<T> _GeneralState;
    protected BaseState<T> _PrevState;

    public T CurrentObject;
    public BaseStateMachine(T currentObject)
    {
        this.CurrentObject = currentObject;
    }
    public void ChangeStateTo<T>(bool force = false)
    {
        for (int i = 0; i < _States.Count; i++) 
        {
            if (_States[i] is T == false)
                continue;
            if (_CurrentState != _States[i] || force)
            {
                _PrevState = _CurrentState;
                _CurrentState.Exit();
                _CurrentState = _States[i];
                _CurrentState.Enter();
            }
            
        }
    }

    private void Enter()
    {
        _CurrentState.Enter();
        _GeneralState.Enter();
    }

    public void Execute()
    {
        _CurrentState.Execute();
        _GeneralState.Execute();
    }

    public void Exit()
    {
        _CurrentState.Exit();
        _GeneralState.Exit();
    }
}
