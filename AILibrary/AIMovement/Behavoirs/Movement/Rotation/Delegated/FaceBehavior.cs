using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement.Delegated;

public class FaceBehavior : AlignBehavior
{

    public Kinematic Target { get; set; } = new();
    public FaceBehavior(float maxAngularAcceleration, float maxRotation, float targetRadius, float slowRadius) : base(maxAngularAcceleration, maxRotation, targetRadius, slowRadius)
    {
    }

    public void SetTargetOrientation(Vector2 target)
    {
        Target.Position = target;
    }
    public override SteeringOutput? GetSteering()
    {
        var direction = Target.Position - Character.Position;

        if (direction.Length() == 0)
        {
            return null;
        }
        
        base.Target = this.Target;
        base.Target.Orientation = (float)Math.Atan2(direction.Y, direction.X);
        return base.GetSteering();
    }
}