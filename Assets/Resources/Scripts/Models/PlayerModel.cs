using System;
using UnityEngine;

public class PlayerModel : IPlayerModel
{
    private int _healt;
    public int Healt 
    {
        get => _healt;
        set
        {
            if (_healt != value)
            {
                _healt = value;
                OnModelHealtChanged?.Invoke(_healt);
                Debug.Log($"Field '{nameof(Healt)}' changed in model");
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
                Debug.Log($"Field '{nameof(ProjectileType)}' changed in model");
            }
        }
    }
    private IProjectileTypeModel _projectileType { get; set; }

    public event Action<int> OnModelHealtChanged;
    public event Action<IProjectileTypeModel> OnModelProjectileTypeChanged;
}
