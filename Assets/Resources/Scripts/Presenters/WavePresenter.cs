using System;
using UnityEngine;

public class WavePresenter : IWavePresenter
{
    private IWaveView _view;
    private IWaveModel _model;
    private IEnemyFactory _enemyFactory;

    public event Action<IWaveModel> OnPresenterWaveCompleted;
    public WavePresenter(IWaveView view, IWaveModel model, IEnemyFactory enemyFactory)
    {
        _view = view;
        _model = model;
        _enemyFactory = enemyFactory;
    }


    public void Dispose()
    {
        
    }

    public void Initialize()
    {
        CreateEnemies();
    }

    public void Start()
    {
        
    }

    private void CreateEnemies()
    {
        foreach (var item in _model.EnemiesCount)
        {
            for (var i = 1; i <= item.Count; i++)
            {
                _enemyFactory.CreateEnemy(i, _model.SpawnPositions[0], _model.TargetPosition, item.EnemyType);
            }
        }
    }

}
