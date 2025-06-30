using UnityEngine;

public class ProjectilePresenter : IProjectilePresenter
{
    private IProjectileView _view;
    private IProjectileModel _model;
    private ProjectileLifeCycleManager _manager;
    public ProjectilePresenter(IProjectileView view, IProjectileModel model, ProjectileLifeCycleManager manager)
    {
        _model = model;
        _view = view;
        _manager = manager;
    }

    public void Initialize()
    {
        // Подписываемся на события View
        _view.OnViewCollider2DTriggered += HandleViewCollision;

        // Устанавливаем начальное состояние View
        _view.SetPosition(_model.Position);
        //float initialAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
        //_view.SetRotation(Quaternion.Euler(0, 0, initialAngle - 90));

        // Регистрируем этот Presenter для получения обновлений
        _manager.RegisterProjectilePresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // Обновляем позицию в модели

        if (_model.IsAlive)
        {
            // Обновляем позицию и поворот представления на основе модели
            _view.SetPosition(_model.Position);
            // Поворот снаряда должен постоянно совпадать с направлением его движения
            float currentAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
            _view.SetRotation(Quaternion.Euler(0, 0, currentAngle - 90));
        }
        else
        {
            DestroyProjectile(); // Если модель "умерла" по времени жизни
        }
    }

    private void HandleViewCollision(Collider2D other)
    {
        // Презентер решает, что делать при столкновении
        if (other.CompareTag("Enemy"))
        {
            // Получаем компонент Enemy. В идеале, это будет EnemyPresenter или EnemyModel,
            // но пока используем существующий Enemy.cs для простоты.
            IEnemyView enemy = other.GetComponent<IEnemyView>();
            if (enemy != null)
            {
                enemy.TakeDamage(_model.Type.Damage); // Модель врага получает урон
            }
        }
        // В любом случае, после столкновения снаряд уничтожается
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // Отписываемся от событий, чтобы избежать утечек памяти
        _view.OnViewCollider2DTriggered -= HandleViewCollision;


        // Отменяем регистрацию этого Presenter из менеджера обновлений
        _manager.UnregisterProjectilePresenter(this);
        // Уничтожаем GameObject представления
        //Object.Destroy(_view.GetGameObject());
    }

    public void Dispose()
    {
        // Логика для очистки ресурсов при уничтожении Presenter'а
        DestroyProjectile(); // Убеждаемся, что все очищено
    }
}
