using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs;

public class ArriveBehavior : BaseBehavior
{
    public float TargetRadius { get; set; }
    public float SlowRadius { get; set; }
    
    private float _timeToTarget = 0.1f;
    
    public float MaxAcceleration { get; set; }
    public float MaxSpeed { get; set; }
    
    public ArriveBehavior(float targetRadius, float slowRadius,float maxSpeed, float maxAcceleration): base(new Kinematic())
    {

        TargetRadius = targetRadius;
        SlowRadius = slowRadius;
        MaxAcceleration = maxAcceleration;
        MaxSpeed = maxSpeed;
    }

    public override SteeringOutput? GetSteering()
    {
        var result = new SteeringOutput();
        
        var direction = Target.Position - Character.Position;
        var distance = direction.Length();
        
        if (distance < TargetRadius)
        {
            return null;
        }

        float targetSpeed;
        
        if (distance > SlowRadius)
        {
            targetSpeed = MaxSpeed;
        }
        else
        {
            targetSpeed = MaxSpeed * distance / SlowRadius;
        }
        
        var targetVelocity = direction;
        targetVelocity = Vector2.Normalize(targetVelocity);
        targetVelocity *= targetSpeed;
        
        result.Linear = targetVelocity - Character.Velocity;
        result.Linear /= _timeToTarget;

        if (result.Linear.Length() > MaxAcceleration)
        {
            result.Linear = Vector2.Normalize(result.Linear);
            result.Linear *= MaxAcceleration;
        }
        
        result.Angular = 0;
        return result;
    }
}