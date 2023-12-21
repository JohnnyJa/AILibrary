using AILibrary.AIMovement.Model;
using AILibrary.AIMovement.Output;
using AILibrary.Static;

namespace AILibrary.AIMovement;

public class KinematicWander : BaseKinematic
{
    private float _maxRotation;

    public KinematicWander(float maxRotation) : base()
    {
        _maxRotation = maxRotation;
    }

    public override SteeringOutput? GetSteering()
    {
        if (Character == null)
        {
            throw new NullReferenceException("Character is null");
        }
        
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