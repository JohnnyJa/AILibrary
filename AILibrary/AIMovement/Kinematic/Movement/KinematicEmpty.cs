using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement;

public class KinematicEmpty : BaseKinematic
{
    public KinematicEmpty() : base()
    {
        
    }
    public override SteeringOutput? GetSteering()
    {
        return null;
    }
}