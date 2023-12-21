using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement;

public class VelocityMatch : BaseBehavior
{

    private readonly float _maxAcceleration;
    private readonly float _timeToTarget = 0.1f;
    
    public VelocityMatch(float maxAcceleration) : base(new Kinematic())
    {
        _maxAcceleration = maxAcceleration;
    }
    public override SteeringOutput? GetSteering()
    {
        var res = new SteeringOutput();
        res.Linear = Target.Velocity - Character.Velocity;
        res.Linear /= _timeToTarget;

        if (res.Linear.Length() > _maxAcceleration)
        {
            res.Linear = Vector2.Normalize(res.Linear);
            res.Linear *= _maxAcceleration;
        }
        res.Angular = 0;
        return res;
    }
}