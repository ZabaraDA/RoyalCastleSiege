using System;
using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    private EnemyLifeCycleManager _manager;

    public event Action<IEnemyModel> OnPresenterEnemyModelDestoyed;
    public event Action<IEnemyPresenter> OnPresenterEnemyPresenterDestoyed;

    public EnemyPresenter(IEnemyView view, IEnemyModel model, EnemyLifeCycleManager manager)
    {
        _view = view;
        _model = model;
        _manager = manager;
    }

    public void Initialize()
    {
        // ������������� �� ������� View
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _view.OnViewTakeDamageTriggered += HandleOnViewTakeDamageTriggered;
        _model.OnModelHealtsChanged += HandleOnModelHealtsChanged;

        // ������������� ��������� ��������� View
        _view.SetPosition(_model.Position);
        _view.SetSprite(_model.Type.Sprite);

        // ������������ ���� Presenter ��� ��������� ����������
        _manager.RegisterEnemyPresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // ��������� ������� � ������

        if (_model.IsAlive)
        {
            _view.SetPosition(_model.Position);
        }
        else
        {
            DestroyEnemy();
        }
    }

    private void HandleOnModelHealtsChanged(int healts)
    {
        if (healts <= 0)
        {
            OnPresenterEnemyModelDestoyed?.Invoke(_model);
            OnPresenterEnemyPresenterDestoyed?.Invoke(this);
            DestroyEnemy();
        }
    }

    private void HandleOnViewTakeDamageTriggered(int damage)
    {
        _model.TakeDamage(damage);
    }
    private void HandleOnViewCollider2DTriggered(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayerView player = other.GetComponent<IPlayerView>();
            if (player != null)
            {
                player.TakeDamage(_model.Type.Damage);
            }
            OnPresenterEnemyModelDestoyed?.Invoke(_model);
            OnPresenterEnemyPresenterDestoyed?.Invoke(this);
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        _view.OnViewCollider2DTriggered -= HandleOnViewCollider2DTriggered;
        _view.OnViewTakeDamageTriggered -= HandleOnViewTakeDamageTriggered;
        _model.OnModelHealtsChanged -= HandleOnModelHealtsChanged;
        _manager.UnregisterEnemyPresenter(this);
        if (_view.GetGameObject() != null)
        {
            MonoBehaviour.Destroy(_view.GetGameObject());        
        }
    }

    public void Dispose()
    {
        
        DestroyEnemy();
    }

    public void TakeDamage(int damage)
    {
        
    }
}
