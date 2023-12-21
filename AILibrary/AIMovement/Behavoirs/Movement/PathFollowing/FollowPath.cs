using AILibrary.AIMovement.Output;

namespace AILibrary.AIMovement.Behavoirs.Movement.PathFollowing;

public class FollowPath : SeekBehavior
{
    public Path.Path Path { get; set; }

    public float PathOffset { get; set; } = 1;
    private float _currentParam;
    
    public FollowPath(Path.Path path, float maxAcceleration) : base(maxAcceleration)
    {
        Path = path;
    }

    public override SteeringOutput? GetSteering()
    {
        _currentParam = Path.GetParam(Character.Position, _currentParam);
        
        var targetParam = _currentParam + PathOffset;
        
        Target.Position = Path.GetPosition(targetParam);
        
        return base.GetSteering();
    }
}