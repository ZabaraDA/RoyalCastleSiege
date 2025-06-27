using UnityEngine;

public interface IEnemyPresenter : IInitializable, IDisposable
{
    void TakeDamage(int damage);
}
