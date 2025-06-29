using UnityEngine;

public interface IProjectileFactory
{
    IProjectilePresenter CreateProjectile(Vector2 startPosition, Vector2 direction, IProjectileModel projectileModel);
}
