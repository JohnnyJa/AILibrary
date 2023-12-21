using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Interface;

public interface IMovement
{
    public void SetTarget(Vector2 target);
    public void SetParams(Kinematic character, float maxSpeed);
    public SteeringOutput? GetSteering();
    public Kinematic Character { get; set; }
    public float MaxSpeed { get; set; }
}