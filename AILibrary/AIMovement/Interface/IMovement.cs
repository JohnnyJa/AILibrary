using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Interface;

public interface IMovement
{
    public SteeringOutput? GetSteering();
}