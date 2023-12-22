using System.Numerics;

namespace AILibrary.AIMovement.Output;

public class SteeringOutput
{
    public Vector2 Linear { get; set; }
    public float Angular { get; set; }
    
    public static SteeringOutput? operator +(SteeringOutput? a, SteeringOutput? b)
    {
        a ??= new SteeringOutput()
        {
            Angular = 0,
            Linear = new Vector2()
        };
        
        
        b ??= new SteeringOutput()
        {
            Angular = 0,
            Linear = new Vector2()
        };
        
        return new SteeringOutput()
        {
            Linear = new Vector2( a.Linear.X + b.Linear.X, a.Linear.Y + b.Linear.Y),
            Angular = a.Angular + b.Angular
        };
    }
}