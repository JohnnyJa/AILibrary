using System.Numerics;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Abstract;

public abstract class BaseBehavior : IBehaviorMovement
{
    protected BaseBehavior(Kinematic? target = null)
    {
        Target = target;
    }
    public Kinematic? Target { get; set; }
    public Kinematic Character { get; set; }

    public void SetCharacter(Kinematic character)
    {
        Character = character;
    }

    public void SetTargetOrientation(Vector2 targetOrientation)
    {
        if (Target == null)
        {
            throw new Exception("Enable to set target to this algo");
        }
        Target.Orientation = GetNewOrientation(Character.Orientation, targetOrientation);
    }


    public abstract SteeringOutput? GetSteering();
    public void SetTargetVelocity(Vector2 targetVelocity)
    {
        if (Target == null)
        {
            throw new Exception("Enable to set target to this algo");
        }
        Target.Velocity = targetVelocity;
    }

    public void SetTargetPosition(Vector2 target)
    {
        if (Target == null)
        {
            throw new Exception("Enable to set target to this algo");
        }
        Target.Position = target;
    }
    
    protected float GetNewOrientation(float currentOrientation, Vector2 velocity)
    {
        if (velocity.LengthSquared() > 0)
        {
            return (float)Math.Atan2(velocity.Y, velocity.X);
        }
        else
        {
            return currentOrientation;
        }
    }
}