namespace AILibrary.DecisionMaking.StateMachine;

public abstract class State
{
    public abstract List<StateAction> GetActions();
    public abstract List<StateAction> GetEntryActions();

    public abstract List<StateAction> GetExitActions();

    public abstract List<Transition> GetTransitions();

}