using System.Net.Mime;
using System.Numerics;
using AILibrary.Movement;
using AILibrary.Static;
using ZeroElectric.Vinculum;

namespace AILibrary.Pawn;

public class Pawn 
{
    private IMovement _movement;
    Texture _texture;
    public float RotationAngle { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public Vector2 TargetPosition { get; set; }
    public float TargetRotation { get; set; }


    public Pawn(Texture texture)
    {
        _texture = texture;
        _movement = new KinematicMovement(1f, 1);
    }
    
    public void Spawn(Vector2 position)
    {
        CurrentPosition = position;
        RotationAngle = 0;
    }

    public virtual void Tick()
    {
        PerformRotation(TargetRotation);
        PerformMovement(TargetPosition);
        Draw();
    }

    private void PerformRotation(float targetRotation)
    {
        RotationAngle = _movement.RotateTo( RotationAngle, targetRotation);
    }
    
    private void PerformMovement(Vector2 targetPosition)
    {
        CurrentPosition = _movement.MoveTo(CurrentPosition, targetPosition);
    }
    
    private void Draw()
    {
        Raylib.DrawTexturePro(_texture,
            new Rectangle(0, 0, _texture.width, _texture.height){  },
            new Rectangle(CurrentPosition.X, CurrentPosition.Y, _texture.width, _texture.height){  },
            new Vector2(_texture.width / 2f, _texture.height / 2f){  },
            RotationAngle * MathHelper.Rad2Deg, // Преобразование радиан в градусы
            Raylib.WHITE);
    }
}