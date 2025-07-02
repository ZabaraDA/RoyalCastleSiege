using System;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveModel
{
    float DelayBetweenSpawnsEnemies { get; set; }
    int Number { get; set; }
    ICollection<IEnemyCountModel> EnemiesCount { get; set; }
    List<Vector2> SpawnPositions { get; set; }
    Vector2 TargetPosition { get; set; }

    event Action<int> OnModelNumberOnChanged;
    void Start();

    
}
