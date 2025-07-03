using UnityEngine;

public class ProjectileTypeModel : IProjectileTypeModel
{
    public int Id { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public float LifeTime { get; private set; }
    public int Cost { get; private set; }

    public Sprite Sprite { get; private set; }

    public ProjectileTypeModel(int id, float speed, int damage, float lifeTime, int cost, Sprite sprite)
    {
        Id = id;
        Cost = cost;
        Speed = speed;
        Damage = damage;
        LifeTime = lifeTime;
        Sprite = sprite;
    }
}
