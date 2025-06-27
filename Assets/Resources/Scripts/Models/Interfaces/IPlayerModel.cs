using System;
using UnityEngine;

public interface IPlayerModel
{
    int Healt { get; set; }
    IProjectileModel Projectile { get; set; }
    event Action<int> OnModelHealtChanged;
    event Action<IProjectileModel> OnModelProjectileChanged;
}
