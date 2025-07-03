using UnityEngine;

public interface IProjectileTypeModel
{
    int Id { get; }
    float Speed { get; }
    int Damage { get; }
    float LifeTime { get; }
    int Cost { get; }
    Sprite Sprite { get; }
}
