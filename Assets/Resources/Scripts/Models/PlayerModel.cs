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

    public IProjectileModel Projectile
    {
        get => _projectile;
        set
        {
            if (_projectile != value)
            {
                _projectile = value;
                OnModelProjectileChanged?.Invoke(_projectile);
                Debug.Log($"Field '{nameof(Projectile)}' changed in model");
            }
        }
    }
    private IProjectileModel _projectile { get; set; }

    public event Action<int> OnModelHealtChanged;
    public event Action<IProjectileModel> OnModelProjectileChanged;
}
