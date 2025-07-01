using System;
using UnityEngine;

public interface IPlayerModel
{
    int Healts { get; set; }
    float FireRate { get; set; } // ��������� � �������
    float NextFireTime { get; set; } // �����, ����� ��������� ������� ����� ��������
    IProjectileTypeModel ProjectileType { get; set; }
    event Action<int> OnModelHealtChanged;
    event Action<IProjectileTypeModel> OnModelProjectileTypeChanged;

    bool CanFire();
    void TakeDamage(int damage);
    void SetLastFireTime(float currentTime); // ��� ������ UpdateFireTime(float currentTime)
}
