namespace AILibrary.DecisionMaking.StateMachine;

public class Transition
{
    public Transition(Condition condition)
    {
        this._condition = condition;
    }
    public StateAction[] Actions { get; set; } = Array.Empty<StateAction>();
    
    public State TargetState { get; set; }

    private readonly Condition _condition;
    
    public bool IsTriggered()
    {
        return _condition.Test();
    }

}