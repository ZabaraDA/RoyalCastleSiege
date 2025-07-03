using UnityEngine;

public class ProjectileModel : IProjectileModel
{
    public int Id { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }

    private float _currentLifetime;
    public bool IsAlive => _currentLifetime < Type.LifeTime;
    public IProjectileTypeModel Type { get; set; }

    public Sprite Sprite => Type.Sprite;

    public ProjectileModel(int id, Vector2 position, Vector2 direction, IProjectileTypeModel type)
    {
        Id = id;
        Type = type;
        Position = position;
        Direction = direction;
        _currentLifetime = 0f;
    }

    public void UpdatePosition(float deltaTime)
    {
        Position += deltaTime * Type.Speed * Direction;
        _currentLifetime += deltaTime;
    }
}
