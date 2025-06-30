using System.Collections.Generic;
using UnityEngine;

public class WaveModel : IWaveModel
{
    public ICollection<IEnemyCountModel> EnemiesCount { get; set; }
    public List<Vector2> SpawnPositions { get; set; }
    public Vector2 TargetPosition { get; set; }
    public int Number { get; set; }

    public WaveModel(int number, ICollection<IEnemyCountModel> enemiesCount, List<Vector2> spawnPositions, Vector2 targetPosition)
    {
        Number = number;
        EnemiesCount = enemiesCount;
        SpawnPositions = spawnPositions;
        TargetPosition = targetPosition;
    }

    public void Start()
    {

    }
}
