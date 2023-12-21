using System.Numerics;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement;

public abstract class BaseKinematic : IMovement
{
    protected BaseKinematic(Kinematic? target = null)
    {
        Target = target;
    }

    public Kinematic? Character { get; set; }
    public Kinematic? Target { get; set; }
    public float MaxSpeed { get; set; }
    public void SetTarget(Vector2 target)
    {
        if (Target == null)
        {
            throw new Exception("Enable to set target to this algo");
        }
        Target.Position = target;
    }

    public void SetParams(Kinematic character, float maxSpeed)
    {
        Character = character;
        MaxSpeed = maxSpeed;
    }

    public abstract SteeringOutput? GetSteering();
    
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