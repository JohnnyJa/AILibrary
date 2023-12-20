using System.Net.Mime;
using System.Numerics;
using AILibrary.Movement;
using AILibrary.Static;
using ZeroElectric.Vinculum;

namespace AILibrary.Pawn;

public class Pawn 
{
    private IMoveable _moveable;
    private Vector2 currentPosition { get; set; }
    Texture _texture;
    private float _rotationAngle;

    public Pawn(Texture texture)
    {
        _texture = texture;
        _moveable = new MoveableBase();
    }
    
    public void Spawn(Vector2 position)
    {
        currentPosition = position;
        _rotationAngle = 0;
    }

    public void Show()
    {
        Raylib.DrawTexturePro(_texture,
            new Rectangle(0, 0, _texture.width, _texture.height){  },
            new Rectangle(currentPosition.X, currentPosition.Y, _texture.width, _texture.height){  },
            new Vector2(_texture.width / 2f, _texture.height / 2f){  },
            _rotationAngle * MathGame.Rad2Deg, // Преобразование радиан в градусы
            Raylib.WHITE);
    }

    public void RotateTo(Vector2 targetRotation)
    {
        _rotationAngle = MathF.Atan2(targetRotation.Y - currentPosition.Y, targetRotation.X - currentPosition.X);
    }
    
}