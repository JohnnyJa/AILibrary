using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement.Delegated;

public class LookWhereYouGoBehavior : AlignBehavior
{
    public LookWhereYouGoBehavior(float maxAngularAcceleration, float maxRotation, float targetRadius, float slowRadius) : base(maxAngularAcceleration, maxRotation, targetRadius, slowRadius)
    {
    }
    
    public override SteeringOutput? GetSteering()
    {
        if (Character.Velocity.Length() == 0)
        {
            return null;
        }
        
        base.Target!.Orientation = (float)Math.Atan2(Character.Velocity.Y, Character.Velocity.X);
        return base.GetSteering();
    }
}