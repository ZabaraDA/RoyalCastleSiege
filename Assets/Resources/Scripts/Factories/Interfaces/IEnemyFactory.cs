using UnityEngine;

public interface IEnemyFactory
{
    IStatisticalItemModel CoinsStatisticalItemModel { get; set; }
    IEnemyPresenter CreateEnemy(int id, Vector2 startPosition, Vector2 direction, IEnemyTypeModel enemyTypeModel);
    IEnemyPresenter CreateEnemy(IEnemyModel enemyModel);
}
