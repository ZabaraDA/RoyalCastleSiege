using UnityEngine;

public class WaveFactory : IWaveFactory
{
    private IEnemyFactory _enemyFactory;
    private MonoBehaviour _corutineRunner;
    //IStatisticalItemModel _wavesStatisticalItemModel;

    public WaveFactory(IEnemyFactory enemyFactory, MonoBehaviour corutineRunner)
    {
        _enemyFactory = enemyFactory;
        _corutineRunner = corutineRunner;
    }
    public IWavePresenter CreateWave(IWaveModel waveModel)
    {

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Wave");
        GameObject projectile = Object.Instantiate(projectilePrefab, Vector2.zero, Quaternion.identity);

        IWaveView view = projectile.GetComponent<IWaveView>();

        IWavePresenter presenter = new WavePresenter(view, waveModel, _enemyFactory, _corutineRunner);
        presenter.Initialize(); // Инициализируем презентер
        //waveModel.OnModelNumberOnChanged += _wavesStatisticalItemModel.SetCount;

        return presenter;
    }
}
