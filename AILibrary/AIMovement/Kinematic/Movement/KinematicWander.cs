using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement;

public class KinematicWander : BaseKinematic
{
    private float _maxRotation;

    public KinematicWander(Kinematic character, float maxSpeed, float maxRotation) : base(character, null, maxSpeed)
    {
        _maxRotation = maxRotation;
    }

    public override SteeringOutput? GetSteering()
    {
        var result = new SteeringOutput
        {
            Linear = MathHelper.AsVector( Character.Orientation)
        };
        result.Linear *= MaxSpeed;
        result.Angular = RandomBinomial() * _maxRotation;
        return result;
    }

    private float RandomBinomial()
    {
        var rand = new Random();
        
        return (float)(rand.NextDouble() - rand.NextDouble());
    }
}