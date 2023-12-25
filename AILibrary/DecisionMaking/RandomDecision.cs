using ZeroElectric.Vinculum;

namespace AILibrary.DecisionMaking;

public class RandomDecision : BooleanDecision
{
    
    float _lastFrame = -1;
    bool _lastDecision = false;
    protected override bool TestValue()
    {
        var frame = Raylib.GetFrameTime();
        if (frame > _lastFrame)
        {
            
            _lastDecision = Random.Shared.NextDouble() < 0.5;
        }
        _lastFrame = frame;
        return _lastDecision;
    }
}
