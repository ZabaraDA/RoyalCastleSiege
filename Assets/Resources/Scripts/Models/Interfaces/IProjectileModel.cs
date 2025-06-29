using UnityEngine;

public interface IProjectileModel
{
    int Id { get; set; }
    int Damage { get; set; }
    int Cost { get; set; }
    Vector2 Position { get; } // Текущая позиция
    Vector2 Direction { get; } // Направление движения
    float Speed { get; }     // Скорость движения
    float Lifetime { get; }  // Максимальное время жизни
    bool IsAlive { get; }    // Жива ли стрела

    void UpdatePosition(float deltaTime);
}
