using UnityEngine;

public class EnemyPresenter : IEnemyPresenter
{
    private IEnemyView _view;
    private IEnemyModel _model;
    public EnemyPresenter(IEnemyView view, IEnemyModel model)
    {
        _view = view;
        _model = model;
    }

    public void Dispose()
    {

    }

    public void Initialize()
    {

    }

    public void TakeDamage(int damage)
    {
        
    }
}
