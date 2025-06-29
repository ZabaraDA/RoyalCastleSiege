using System;
using UnityEngine;

public class PlayerPresenter : IPlayerPresenter
{
    private IPlayerModel _model;
    private IPlayerView _view;
    private IProjectileFactory _projectileFactory;
    public PlayerPresenter(IPlayerView view, IPlayerModel model, IProjectileFactory projectileFactory)
    {
        _model = model;
        _view = view;
        _projectileFactory = projectileFactory;
    }

    public void Dispose()
    {

    }

    public void Initialize()
    {
        _view.OnViewMouseButtonClick += OnViewMouseButtonClick;
    }

    private void OnViewMouseButtonClick(Vector3 clickPosition, Vector3 firePosition)
    {
        // Ќаправление снар€да от FirePoint до позиции клика
        Vector2 projectileDirection = (clickPosition - firePosition).normalized;

        //float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
        //Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        //GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Projectile");
        //GameObject projectile = MonoBehaviour.Instantiate(projectilePrefab, firePosition, quaternion);

        //IProjectileView projectileView = projectile.GetComponent<IProjectileView>();
        //IProjectileModel projectileModel = _model.Projectile;
        //IProjectilePresenter projectilePresenter = new ProjectilePresenter(projectileView, projectileModel);

        //projectilePresenter.Initialize();

        _projectileFactory.CreateProjectile(firePosition, projectileDirection, _model.Projectile);
        
    }
}
