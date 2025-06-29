using UnityEngine;

public class ProjectileModel : IProjectileModel
{
    public int Id { get; set; }
    public int Damage { get; set; }
    public int Cost { get; set; }
    public Vector2 Position { get; private set; }
    public Vector2 Direction { get; private set; }
    public float Speed { get; private set; }
    public float Lifetime { get; private set; }

    private float _currentLifetime;
    public bool IsAlive => _currentLifetime < Lifetime;

    public ProjectileModel(int id, float speed, int damage, float lifetime)
    {
        Id = id;
        Speed = speed;
        Damage = damage;
        Lifetime = lifetime;
        _currentLifetime = 0f;
    }

    public void UpdatePosition(float deltaTime)
    {
        Position += Direction * Speed * deltaTime;
        _currentLifetime += deltaTime;
    }
}
