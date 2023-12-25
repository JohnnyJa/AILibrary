using ZeroElectric.Vinculum;

namespace AILibrary.DecisionMaking;

public class RandomDecisionWithTimeout : BooleanDecision
{
    float _lastFrame = -1;
    bool _lastDecision = false;
    
    public float Timeout { get; set; } = 1000f;
    private float TimeoutFrame = -1;
    protected override bool TestValue()
    {
        var frame = Raylib.GetFrameTime();
        if (frame > _lastFrame + 1 || frame >= TimeoutFrame)
        {
            _lastDecision = Random.Shared.NextDouble() < 0.5;
            TimeoutFrame = frame + Timeout;
        }
        _lastFrame = frame;
        return _lastDecision;
    }
}