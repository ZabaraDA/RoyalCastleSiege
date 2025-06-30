using System;
using UnityEngine;

public interface IPlayerModel
{
    int Healt { get; set; }
    IProjectileTypeModel ProjectileType { get; set; }
    event Action<int> OnModelHealtChanged;
    event Action<IProjectileTypeModel> OnModelProjectileTypeChanged;
}
