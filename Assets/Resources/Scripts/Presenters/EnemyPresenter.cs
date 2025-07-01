using System;
using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    private EnemyLifeCycleManager _manager;

    public event Action<IEnemyModel> OnPresenterEnemyDestoyed;

    public EnemyPresenter(IEnemyView view, IEnemyModel model, EnemyLifeCycleManager manager)
    {
        _view = view;
        _model = model;
        _manager = manager;
    }

    public void Initialize()
    {
        // Подписываемся на события View
        _view.OnViewCollider2DTriggered += HandleOnViewCollider2DTriggered;
        _view.OnViewTakeDamageTriggered += HandleOnViewTakeDamageTriggered;
        _model.OnModelHealtsChanged += HandleOnModelHealtsChanged;

        // Устанавливаем начальное состояние View
        _view.SetPosition(_model.Position);

        // Регистрируем этот Presenter для получения обновлений
        _manager.RegisterEnemyPresenter(this);
    }

    public void Update(float deltaTime)
    {
        _model.UpdatePosition(deltaTime); // Обновляем позицию в модели

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
            OnPresenterEnemyDestoyed?.Invoke(_model);
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
