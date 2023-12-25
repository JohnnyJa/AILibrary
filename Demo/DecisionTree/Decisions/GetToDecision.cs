using System.Numerics;
using AILibrary.DecisionMaking;

namespace HelloWorld.DecisionTree.Decisions;

public class GetToDecision : BooleanDecision
{
    public Vector2 CurrentPosition { get; set; }
    public Vector2 TargetPosition { get; set; }
    public float AcceptRadius { get; set; }

    protected override bool TestValue()
    {
        if ((TargetPosition - CurrentPosition).Length() < AcceptRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}