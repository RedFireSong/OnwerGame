public class StateBase
{
    protected GameRoot gameRoot;
    protected StateType stateType = StateType.None;

    public StateBase(StateType _stateType)
    {
        gameRoot = GameRoot.Instance;
        stateType = _stateType;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
