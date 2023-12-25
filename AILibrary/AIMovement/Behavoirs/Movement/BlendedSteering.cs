using System.Numerics;
using AILibrary.AIMovement.Behavoirs.Abstract;
using AILibrary.AIMovement.Interface;
using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement;

public class BlendedSteering : BaseBehavior
{
    public class BehaviorAndWeight
    {
        public IBehaviorMovement Behavior { get; set; }
        public float Weight { get; set; }
    }

    public BehaviorAndWeight[] Behaviors { get; set; }
    public float MaxAcceleration { get; set; }
    public float MaxRotation { get; set; }

    public override SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Accumulate all accelerations.
        foreach (var behaviorAndWeight in Behaviors)
        {
            var steering = behaviorAndWeight.Behavior.GetSteering();
            result.Linear += behaviorAndWeight.Weight * steering?.Linear ?? new Vector2();
            if (steering.Angular == float.NaN)
            {
                result.Angular = float.NaN;
            }
            else
            {
                result.Angular += behaviorAndWeight.Weight * steering?.Angular ?? 0;
            }
        }

        return result;
    }
}