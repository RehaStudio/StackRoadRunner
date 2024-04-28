
public abstract class BaseState<T>
{
    public T CurrentObject { get; private set; }
    public BaseStateMachine<T> BaseStateMachine { get; private set; }
    public BaseState(T currentObject,BaseStateMachine<T> baseStateMachine)
    {
        this.CurrentObject = currentObject;
        this.BaseStateMachine = baseStateMachine;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Execute();
    public abstract void OnEvent();
}
