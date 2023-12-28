using AILibrary.DecisionMaking.StateMachine;
using ZeroElectric.Vinculum;

namespace HelloWorld.StateMachine;

public class OnLeftButtonPressedCondition : BooleanCondition
{
    protected override bool TestValue()
    {
        return Raylib.IsMouseButtonPressed(Raylib.MOUSE_LEFT_BUTTON);
    }
}