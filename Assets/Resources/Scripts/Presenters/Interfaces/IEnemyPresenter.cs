using System;
using UnityEngine;

public interface IEnemyPresenter : IInitializable, IDisposable, IUpdatable
{
    event Action<IEnemyModel> OnPresenterEnemyDestoyed;
    void TakeDamage(int damage);
}
