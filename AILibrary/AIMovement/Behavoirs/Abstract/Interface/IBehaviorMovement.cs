using System.Numerics;
using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Interface;

public interface IBehaviorMovement
{
    public void SetTargetPosition(Vector2 target);
    public void SetCharacter(Kinematic character);
    public void SetTargetOrientation(float targetOrientation);
    
    public SteeringOutput? GetSteering();
    void SetTargetVelocity(Vector2 targetVelocity);
}