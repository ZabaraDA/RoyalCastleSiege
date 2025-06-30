using UnityEngine;

public interface IEnemyFactory
{
    IEnemyPresenter CreateEnemy(int id, Vector2 startPosition, Vector2 direction, IEnemyTypeModel enemyTypeModel);
    IEnemyPresenter CreateEnemy(IEnemyModel enemyModel);
}
