using UnityEngine;

public interface IEnemyCountModel
{
    int Count { get; set; }
    IEnemyTypeModel EnemyType { get; set; }
}
