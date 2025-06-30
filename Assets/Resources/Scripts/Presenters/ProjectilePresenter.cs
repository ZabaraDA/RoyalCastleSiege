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
        // ѕодписываемс€ на событи€ View
        _view.OnViewCollider2DTriggered += HandleViewCollision;

        // ”станавливаем начальное состо€ние View
        _view.SetPosition(_model.Position);
        //float initialAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
        //_view.SetRotation(Quaternion.Euler(0, 0, initialAngle - 90));

        // –егистрируем этот Presenter дл€ получени€ обновлений
        _manager.RegisterProjectilePresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // ќбновл€ем позицию в модели

        if (_model.IsAlive)
        {
            // ќбновл€ем позицию и поворот представлени€ на основе модели
            _view.SetPosition(_model.Position);
            // ѕоворот снар€да должен посто€нно совпадать с направлением его движени€
            float currentAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
            _view.SetRotation(Quaternion.Euler(0, 0, currentAngle - 90));
        }
        else
        {
            DestroyProjectile();
        }
    }

    private void HandleViewCollision(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IEnemyView enemy = other.GetComponent<IEnemyView>();
            if (enemy != null)
            {
                enemy.TakeDamage(_model.Type.Damage);
            }
        }
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        _view.OnViewCollider2DTriggered -= HandleViewCollision;
        _manager.UnregisterProjectilePresenter(this);

    }

    public void Dispose()
    {
        DestroyProjectile();
    }
}
