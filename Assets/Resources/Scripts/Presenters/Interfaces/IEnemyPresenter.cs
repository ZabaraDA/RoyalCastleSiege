using UnityEngine;

public interface IEnemyPresenter : IInitializable, IDisposable, IUpdatable
{
    void TakeDamage(int damage);
}
