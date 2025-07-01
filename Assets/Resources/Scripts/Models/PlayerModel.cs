using System;
using UnityEngine;

public class PlayerModel : IPlayerModel
{
    private int _healt;
    public int Healts 
    {
        get => _healt;
        set
        {
            if (_healt != value)
            {
                _healt = value;
                OnModelHealtChanged?.Invoke(_healt);
                Debug.Log($"Field '{nameof(Healts)}' changed in class {GetType()}. Value: {Healts}");
            }
        }
    }

    public IProjectileTypeModel ProjectileType
    {
        get => _projectileType;
        set
        {
            if (_projectileType != value)
            {
                _projectileType = value;
                OnModelProjectileTypeChanged?.Invoke(_projectileType);
                Debug.Log($"Field '{nameof(ProjectileType)}' changed in class {GetType()}. Value: {ProjectileType}");
            }
        }
    }
    private IProjectileTypeModel _projectileType { get; set; }

    public event Action<int> OnModelHealtChanged;
    public event Action<IProjectileTypeModel> OnModelProjectileTypeChanged;

    public float FireRate { get; set; } = 2f; // Значение по умолчанию
    public float NextFireTime { get; set; } // Инициализируется Presenter'ом или при создании модели

    public bool CanFire()
    {
        return Time.time >= NextFireTime;
    }

    public void SetLastFireTime(float currentTime)
    {
        NextFireTime = currentTime + (1f / FireRate);
    }

    public void TakeDamage(int damage)
    {
        Healts -= damage;
    }
}
