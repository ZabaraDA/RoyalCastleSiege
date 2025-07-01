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
        _view.OnViewMouseButtonClick += HandleOnViewMouseButtonClick;
        _view.OnViewTakeDamageTriggered += HandleOnViewTakeDamageTriggered;
    }

    private void HandleOnViewTakeDamageTriggered(int damage)
    {
        _model.TakeDamage(damage);
    }
    private void HandleOnViewMouseButtonClick(Vector3 clickPosition, Vector3 firePosition)
    {
        // Логика кулдауна теперь вызывается из модели
        if (_model.CanFire())
        {
            // Разрешаем выстрел
            Vector2 projectileDirection = (clickPosition - firePosition).normalized;
            _projectileFactory.CreateProjectile(1, firePosition, projectileDirection, _model.ProjectileType);

            // Обновляем время следующего разрешенного выстрела в модели
            _model.SetLastFireTime(Time.time);

            Debug.Log("Выстрел произведен.Следующий выстрел возможен в: " + _model.NextFireTime);
        }
        else
        {
            Debug.Log("Невозможно стрелять. Кулдаун активен. Осталось: " + (_model.NextFireTime - Time.time).ToString("F2") + "с");
        }
    }
}
