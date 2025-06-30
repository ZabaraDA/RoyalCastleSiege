using UnityEngine;

public interface IProjectileModel
{
    int Id { get; set; }
    bool IsAlive { get; } // Жива ли стрела
    Vector2 Position { get; set; }
    Vector2 Direction { get; set; }
    IProjectileTypeModel Type { get; set; }

    void UpdatePosition(float deltaTime);
}
