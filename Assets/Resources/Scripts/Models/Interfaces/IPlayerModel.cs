using System;
using UnityEngine;

public interface IPlayerModel
{
    int Healts { get; set; }
    float FireRate { get; set; } // Выстрелов в секунду
    float NextFireTime { get; set; } // Время, когда следующий выстрел будет разрешен
    IProjectileTypeModel ProjectileType { get; set; }
    event Action<int> OnModelHealtChanged;
    event Action<IProjectileTypeModel> OnModelProjectileTypeChanged;

    bool CanFire();
    void TakeDamage(int damage);
    void SetLastFireTime(float currentTime); // Или просто UpdateFireTime(float currentTime)
}
