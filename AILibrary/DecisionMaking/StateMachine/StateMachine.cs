namespace AILibrary.DecisionMaking.StateMachine;

public class StateMachine
{
    State _initialState;
    State currentState;
    
    public StateMachine(State initialState)
    {
        this._initialState = initialState;
        this.currentState = initialState;
    }

    public StateAction[] Update()
    {
        Transition? triggered = null;

        foreach (var transition in currentState.GetTransitions())
        {
            if (transition.IsTriggered())
            {
                triggered = transition;
                break;
            }
        }

        if (triggered != null)
        {
            var targetState = triggered.TargetState;

            var actions = currentState.GetExitActions();
            actions.AddRange(triggered.Actions);
            actions.AddRange(targetState.GetEntryActions());

            currentState = targetState;
            return actions.ToArray();
        }
        else
        {
            return currentState.GetActions().ToArray();
        }
    }
}