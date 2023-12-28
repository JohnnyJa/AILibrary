using AILibrary.DecisionMaking.StateMachine;
using ZeroElectric.Vinculum;

namespace HelloWorld.StateMachine;

public class OnLeftButtonRealisedCondition : BooleanCondition
{
    protected override bool TestValue()
    {
        return Raylib.IsMouseButtonReleased(Raylib.MOUSE_LEFT_BUTTON);

    }
}