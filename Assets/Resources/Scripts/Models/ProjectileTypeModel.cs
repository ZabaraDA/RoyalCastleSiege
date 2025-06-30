using UnityEngine;

public class ProjectileTypeModel : IProjectileTypeModel
{
    public int Id { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public float LifeTime { get; private set; }

    // Конструктор только для свойств ТИПА
    public ProjectileTypeModel(int id, float speed, int damage, float lifeTime)
    {
        Id = id;
        Speed = speed;
        Damage = damage;
        LifeTime = lifeTime;
    }
}
