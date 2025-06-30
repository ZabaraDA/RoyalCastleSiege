using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    private EnemyLifeCycleManager _manager;
    public EnemyPresenter(IEnemyView view, IEnemyModel model, EnemyLifeCycleManager manager)
    {
        _view = view;
        _model = model;
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
        _manager.RegisterEnemyPresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // ��������� ������� � ������

        if (_model.IsAlive)
        {
            // ��������� ������� � ������� ������������� �� ������ ������
            _view.SetPosition(_model.Position);
            // ������� ������� ������ ��������� ��������� � ������������ ��� ��������
            //float currentAngle = Mathf.Atan2(_model.TargetPosition.y, _model.TargetPosition.x) * Mathf.Rad2Deg;
            //_view.SetRotation(Quaternion.Euler(0, 0, currentAngle - 90));
        }
        else
        {
            DestroyEnemy();
        }
    }

    private void HandleViewCollision(Collider2D other)
    {
        //if (other.CompareTag("Projectile"))
        //{
        //    IEnemyView enemy = other.GetComponent<IEnemyView>();
        //    if (enemy != null)
        //    {
        //        enemy.TakeDamage(_model.Type.Damage);
        //    }
        //}
        //DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        _view.OnViewCollider2DTriggered -= HandleViewCollision;
        _manager.UnregisterEnemyPresenter(this);

    }

    public void Dispose()
    {
        DestroyEnemy();
    }

    public void TakeDamage(int damage)
    {
        
    }
}
