using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement.Behavoirs.Movement.Delegated;

public class WanderBehavior : FaceBehavior
{
    private readonly float _wanderOffset;
    private readonly float _wanderRadius;

    private readonly float _wanderRate;
    private float _wanderOrientation;
    private readonly float _maxAcceleration;

    public WanderBehavior(float maxAngularAcceleration, float maxRotation, float targetRadius, float slowRadius,
        float wanderOffset, float wanderRadius, float wanderRate, float wanderOrientation,
        float maxAcceleration) : base(
        maxAngularAcceleration, maxRotation, targetRadius, slowRadius)
    {
        _wanderOffset = wanderOffset;
        _wanderRadius = wanderRadius;
        _wanderRate = wanderRate;
        _wanderOrientation = wanderOrientation;
        _maxAcceleration = maxAcceleration;
    }

    public override SteeringOutput? GetSteering()
    {
        _wanderOrientation += RandomBinomial() * _wanderRate;

        var targetOrientation = _wanderOrientation + Character.Orientation;
        var target = Character.Position + _wanderOffset * MathHelper.AsVector(Character.Orientation);
        target += _wanderRadius * MathHelper.AsVector(targetOrientation);


        var res = base.GetSteering();

        if (res == null)
        {
            Character.Rotation = 0;
        }

        return new SteeringOutput()
        {
            Angular = res?.Angular ?? 0,
            Linear = _maxAcceleration * MathHelper.AsVector(Character.Orientation),
        };
        
    }
}