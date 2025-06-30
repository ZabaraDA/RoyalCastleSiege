using UnityEngine;

public class EnemyCountModel : IEnemyCountModel
{
    public int Count { get; set; }
    public IEnemyTypeModel EnemyType { get; set; }

    public EnemyCountModel(IEnemyTypeModel enemyType, int count)
    {
        Count = count;
        EnemyType = enemyType;
    }
}
