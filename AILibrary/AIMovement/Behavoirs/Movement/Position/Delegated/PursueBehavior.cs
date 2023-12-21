using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement.Delegated;

public class PursueBehavior : SeekBehavior
{
    private float _maxPrediction;
    public Kinematic Target { get; set; } = new();
    public PursueBehavior(float maxPrediction, float maxAcceleration ) : base(maxAcceleration)
    {
        _maxPrediction = maxPrediction;
    }

    public override void SetTargetPosition(Vector2 targetOrientation)
    {
        Target.Position = targetOrientation;
    }

    public override SteeringOutput? GetSteering()
    {
        var direction = Target.Position - Character.Position;
        var distance = direction.Length();
        
        float speed = Character.Velocity.Length();
        float prediction;
        if (speed <= distance / _maxPrediction)
        {
            prediction = _maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        base.Target = this.Target;
        base.Target.Position += Target.Velocity * prediction;
        
        return base.GetSteering();
    }
}