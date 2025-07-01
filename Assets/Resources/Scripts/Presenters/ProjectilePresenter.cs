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
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _view.SetPosition(_model.Position);

        _manager.RegisterProjectilePresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // ��������� ������� � ������

        if (_model.IsAlive)
        {
            // ��������� ������� � ������� ������������� �� ������ ������
            _view.SetPosition(_model.Position);
            // ������� ������� ������ ��������� ��������� � ������������ ��� ��������
            float currentAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
            _view.SetRotation(Quaternion.Euler(0, 0, currentAngle - 90));
        }
        else
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        _view.OnViewCollider2DTriggered -= HandleOnViewCollider2DTriggered;
        _manager.UnregisterProjectilePresenter(this);
        if (_view.GetGameObject() != null)
        {
            MonoBehaviour.Destroy(_view.GetGameObject());
        }
    }
    private void HandleOnViewCollider2DTriggered(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<IEnemyView>(out var enemy))
            {
                enemy.TakeDamage(_model.Type.Damage);
            }
            DestroyEnemy();
        }
    }

    public void Dispose()
    {
        DestroyEnemy();
    }
}
