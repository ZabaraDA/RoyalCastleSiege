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
        _projectileFactory.CreateProjectile(1, firePosition, projectileDirection, _model.ProjectileType);
        
    }
}
