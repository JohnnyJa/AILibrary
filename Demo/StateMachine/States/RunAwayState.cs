using AILibrary.DecisionMaking.StateMachine;
using AILibrary.Pawn;
using HelloWorld.StateMachine.Actions;

namespace HelloWorld.StateMachine.States;

public class RunAwayState : State
{
    private Pawn _pawn;
    
    public RunAwayState(Pawn pawn)
    {
        _pawn = pawn;
    }
    public override List<StateAction> GetActions()
    {
        return new List<StateAction>() { new RunAwayAction(_pawn) };
    }

    public override List<StateAction> GetEntryActions()
    {
        return new List<StateAction>();
    }

    public override List<StateAction> GetExitActions()
    {
        return new List<StateAction>();

    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>()
        {
            new Transition(new OnLeftButtonRealisedCondition())
            {
                TargetState = new PatrolState(_pawn),
            }
        };
    }
}