using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveModel : IWaveModel
{
    public ICollection<IEnemyCountModel> EnemiesCount { get; set; }
    public List<Vector2> SpawnPositions { get; set; }
    public Vector2 TargetPosition { get; set; }
    public int Number { get; set; }
    public float DelayBetweenSpawnsEnemies { get; set; }

    public event Action<int> OnModelNumberOnChanged;
    public WaveModel(int number, float delayBetweenSpawnsEnemies, ICollection<IEnemyCountModel> enemiesCount, List<Vector2> spawnPositions, Vector2 targetPosition)
    {
        Number = number;
        EnemiesCount = enemiesCount;
        SpawnPositions = spawnPositions;
        TargetPosition = targetPosition;
        DelayBetweenSpawnsEnemies = delayBetweenSpawnsEnemies;
    }


    public void Start()
    {

    }
}
