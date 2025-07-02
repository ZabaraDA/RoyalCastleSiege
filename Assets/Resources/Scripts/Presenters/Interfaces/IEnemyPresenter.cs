using System;
using UnityEngine;

public interface IEnemyPresenter : IInitializable, IDisposable, IUpdatable
{
    event Action<IEnemyModel> OnPresenterEnemyModelDestoyed;
    event Action<IEnemyPresenter> OnPresenterEnemyPresenterDestoyed;
    void TakeDamage(int damage);
}
