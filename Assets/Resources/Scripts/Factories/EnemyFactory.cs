using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactory : IEnemyFactory
{
    private EnemyLifeCycleManager _manager;

    public EnemyFactory(EnemyLifeCycleManager manager, IStatisticalItemModel coinsStatisticalItemModel)
    { 
        _manager = manager;
        CoinsStatisticalItemModel = coinsStatisticalItemModel;
    }

    public IStatisticalItemModel CoinsStatisticalItemModel { get; set; }

    public IEnemyPresenter CreateEnemy(int id, Vector2 startPosition, Vector2 direction, IEnemyTypeModel enemyTypeModel)
    {
        IEnemyModel model = new EnemyModel(id, startPosition, direction, enemyTypeModel);
        return CreateEnemy(model);
    }

    public IEnemyPresenter CreateEnemy(IEnemyModel enemyModel)
    {

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Enemy");
        GameObject projectile = Object.Instantiate(projectilePrefab, enemyModel.Position, Quaternion.identity);

        IEnemyView view = projectile.GetComponent<IEnemyView>();

        IEnemyPresenter presenter = new EnemyPresenter(view, enemyModel, _manager);
        presenter.Initialize(); // Инициализируем презентер
        presenter.OnPresenterEnemyModelDestoyed += (model) => CoinsStatisticalItemModel.AddCount(model.Type.Prize);

        return presenter;
    }
}
