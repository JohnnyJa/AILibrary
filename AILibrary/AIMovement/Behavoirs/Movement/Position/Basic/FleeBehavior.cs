using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs;

public class FleeBehavior : BaseBehavior
{
    public FleeBehavior(float maxAcceleration): base(new Kinematic())
    {
        MaxAcceleration = maxAcceleration;
    }
    
    public float MaxAcceleration { get; set; }
    public override SteeringOutput? GetSteering()
    {
        var result = new SteeringOutput();
        result.Linear = Character.Position - Target.Position;
        
        if (result.Linear.LengthSquared() < 1)
        {
            result.Linear = Vector2.Zero;
        }
        else
        {
            result.Linear = Vector2.Normalize(result.Linear);
            result.Linear *= MaxAcceleration;
        }
        
        result.Angular = 0;
        return result;
    }
}