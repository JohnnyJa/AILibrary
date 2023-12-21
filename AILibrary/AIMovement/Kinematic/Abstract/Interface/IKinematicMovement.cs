using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Interface;

public interface IKinematicMovement
{
    public void SetTarget(Vector2 target);
    public void SetParams(Kinematic character, float maxSpeed);
    public SteeringOutput? GetSteering();
}