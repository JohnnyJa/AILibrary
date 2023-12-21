using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement;

public class EmptyBehavior : BaseBehavior
{
    public EmptyBehavior() : base()
    {
        
    }
    public override SteeringOutput? GetSteering()
    {
        return null;
    }
}