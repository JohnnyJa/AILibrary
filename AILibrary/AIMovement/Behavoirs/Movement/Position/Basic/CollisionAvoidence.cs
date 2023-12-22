using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement.Position.Basic;

public class CollisionAvoidence : BaseBehavior
{
    private readonly float _maxAcceleration;
    
    public List<Kinematic> Targets { get; set; } = new();
    
    private readonly float _radius;

    public CollisionAvoidence(float maxAcceleration, float radius)
    {
        _maxAcceleration = maxAcceleration;
        _radius = radius;
    }

    public override SteeringOutput? GetSteering()
    {
        var shortestTime = float.MaxValue;
        var firstTarget = -1;
        var firstMinSeparation = 0f;
        var firstDistance = 0f;
        var firstRelativePos = Vector2.Zero;
        var firstRelativeVel = Vector2.Zero;
        
        Vector2 relativePos;

        foreach (var target in Targets)
        {
            relativePos = Character.Position - target.Position;
            var relativeVel = Character.Velocity - target.Velocity;
            var relativeSpeed = relativeVel.Length();
            var dotProduct = Vector2.Dot(relativePos, relativeVel);
            var timeToCollision = Math.Abs(dotProduct) / (relativeSpeed * relativeSpeed);
            
            var distance = relativePos.Length();
            var minSeparation = distance - relativeSpeed * timeToCollision;
            
            if (minSeparation > 2 * _radius)
            {
                continue;
            }
            
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = Targets.IndexOf(target);
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        if (firstTarget == -1)
        {
            return null;
        }

        if (firstMinSeparation <= 0 || firstDistance < 2 * _radius)
        {
            relativePos = Targets[firstTarget].Position - Character.Position;
        }
        else
        {
            relativePos = firstRelativePos + firstRelativeVel * shortestTime;
        }

        relativePos = Vector2.Normalize(relativePos);
        
        var res = new SteeringOutput();
        res.Linear = relativePos * _maxAcceleration;
        res.Angular = 0;
        return res;
    }
}