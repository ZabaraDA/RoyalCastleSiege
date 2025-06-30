using UnityEngine;

public class WaveFactory : IWaveFactory
{
    private IEnemyFactory _enemyFactory;

    public WaveFactory(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }
    public IWavePresenter CreateWave(IWaveModel waveModel)
    {

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Wave");
        GameObject projectile = Object.Instantiate(projectilePrefab, Vector2.zero, Quaternion.identity);

        IWaveView view = projectile.GetComponent<IWaveView>();

        IWavePresenter presenter = new WavePresenter(view, waveModel, _enemyFactory);
        presenter.Initialize(); // Инициализируем презентер

        return presenter;
    }
}
