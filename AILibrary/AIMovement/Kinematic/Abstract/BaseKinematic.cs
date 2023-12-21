using System.Numerics;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement;

public abstract class BaseKinematic : IMovement
{
    protected BaseKinematic(Kinematic character, Kinematic? target, float maxSpeed)
    {
        Character = character;
        Target = target;
        MaxSpeed = maxSpeed;
    }

    public Kinematic Character { get; set; }
    public Kinematic? Target { get; set; }
    public float MaxSpeed { get; set; }
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