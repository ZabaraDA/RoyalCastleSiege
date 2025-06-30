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
        // ������������� �� ������� View
        _view.OnViewCollider2DTriggered += HandleViewCollision;

        // ������������� ��������� ��������� View
        _view.SetPosition(_model.Position);
        //float initialAngle = Mathf.Atan2(_model.Direction.y, _model.Direction.x) * Mathf.Rad2Deg;
        //_view.SetRotation(Quaternion.Euler(0, 0, initialAngle - 90));

        // ������������ ���� Presenter ��� ��������� ����������
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
            DestroyProjectile(); // ���� ������ "������" �� ������� �����
        }
    }

    private void HandleViewCollision(Collider2D other)
    {
        // ��������� ������, ��� ������ ��� ������������
        if (other.CompareTag("Enemy"))
        {
            // �������� ��������� Enemy. � ������, ��� ����� EnemyPresenter ��� EnemyModel,
            // �� ���� ���������� ������������ Enemy.cs ��� ��������.
            IEnemyView enemy = other.GetComponent<IEnemyView>();
            if (enemy != null)
            {
                enemy.TakeDamage(_model.Type.Damage); // ������ ����� �������� ����
            }
        }
        // � ����� ������, ����� ������������ ������ ������������
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // ������������ �� �������, ����� �������� ������ ������
        _view.OnViewCollider2DTriggered -= HandleViewCollision;


        // �������� ����������� ����� Presenter �� ��������� ����������
        _manager.UnregisterProjectilePresenter(this);
        // ���������� GameObject �������������
        //Object.Destroy(_view.GetGameObject());
    }

    public void Dispose()
    {
        // ������ ��� ������� �������� ��� ����������� Presenter'�
        DestroyProjectile(); // ����������, ��� ��� �������
    }
}
