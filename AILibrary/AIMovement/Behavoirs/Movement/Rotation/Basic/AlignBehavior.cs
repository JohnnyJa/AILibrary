using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement.Behavoirs.Movement;

public class AlignBehavior : BaseBehavior
{
    readonly float _maxAngularAcceleration;
    readonly float _maxRotation;
    readonly float _targetRadius;
    readonly float _slowRadius;
    readonly float _timeToTarget = 0.1f;
    public AlignBehavior(float maxAngularAcceleration, float maxRotation, float targetRadius, float slowRadius) : base(new Kinematic())
    {
        _maxAngularAcceleration = maxAngularAcceleration;
        _maxRotation = maxRotation;
        _targetRadius = targetRadius;
        _slowRadius = slowRadius;
    }

    public override SteeringOutput? GetSteering()
    {
        var result = new SteeringOutput();
        Console.WriteLine("Target.Orientation: " + Target.Orientation);
        var rotation = Target.Orientation - Character.Orientation;
        
        rotation = MathHelper.WrapAngle(rotation);
        var rotationSize = Math.Abs(rotation);
        
        Console.WriteLine($"rotationSize: {rotationSize}, _targetRadius: {_targetRadius}");
        
        if (rotationSize < _targetRadius)
        {
            return new SteeringOutput()
            {
                Linear = Vector2.Zero,
                Angular = float.NaN
            };
        }

        float targetRotation;
        
        if (rotationSize > _slowRadius)
        {
            targetRotation = _maxRotation;
        }
        else
        {
            targetRotation = _maxRotation * rotationSize / _slowRadius;
        }

        
        targetRotation *= rotation / rotationSize;
        
        result.Angular = targetRotation - Character.Rotation;
        result.Angular /= _timeToTarget;
        
        var angularAcceleration = Math.Abs(result.Angular);
        if (angularAcceleration > _maxAngularAcceleration)
        {
            result.Angular /= angularAcceleration;
            result.Angular *= _maxAngularAcceleration;
        }
        
        result.Linear = Vector2.Zero;
        return result;
    }
}