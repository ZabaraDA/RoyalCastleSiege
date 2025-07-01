using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _gameContainerGameObject;
    [SerializeField]
    private GameObject _playerGameObject;
    [SerializeField]
    private ProjectileLifeCycleManager _projectileLifeCycleManager;
    [SerializeField]
    private EnemyLifeCycleManager _enemyLifeCycleManager;
    [SerializeField]
    private GameObject _spawnContainer;

    [SerializeField]
    private StatisticalItemView _levelStatisticalItemView;
    [SerializeField]
    private StatisticalItemView _coinsStatisticalItemView;
    [SerializeField]
    private StatisticalItemView _wavesStatisticalItemView;
    [SerializeField]
    private StatisticalItemView _healtsStatisticalItemView;



    [SerializeField]
    private float _delayBetweenSpawnsEnemies = 1.5f;

    private void Start()
    {
        ICollection<IEnemyTypeModel> enemyTypes = new List<IEnemyTypeModel>();
        foreach (var type in GetEnemyTypesInJson())
        {
            enemyTypes.Add(new EnemyTypeModel
            {
                Id = type.Id,
                Damage = type.Damage,
                Healt = type.Healt,
                Speed = type.Speed,
                Prize = type.Prize,
            });
        }

        Debug.Log("Enemy types: " + enemyTypes.Count);
        ICollection<IProjectileTypeModel> projectileModels = new List<IProjectileTypeModel>();
        foreach (ProjectileTypeData projectileType in GetProjectileTypesInJson())
        {
            projectileModels.Add(new ProjectileTypeModel(projectileType.Id, projectileType.Speed, projectileType.Damage, 100));
        }

        ICollection<IWaveModel> waveModels = new List<IWaveModel>();

        List<Vector2> spawnPosition = new List<Vector2>();

        foreach (Transform child in _spawnContainer.transform)
        {
            Vector3 childWorldPosition = child.position;
            Vector2 childPosition2D = new Vector2(childWorldPosition.x, childWorldPosition.y);

            spawnPosition.Add(childPosition2D);
        }

        foreach (var wave in GetWavesInJson())
        {
            ICollection<IEnemyCountModel> enemyModels = new List<IEnemyCountModel>();
            foreach (var enemyCount in wave.EnemyCountList)
            {
                IEnemyTypeModel enemyTypeModel = enemyTypes.FirstOrDefault(x => x.Id == enemyCount.EnemyTypeId);
                IEnemyCountModel enemyModel = new EnemyCountModel(enemyTypeModel, enemyCount.Count);
                enemyModels.Add(enemyModel);
            }
            IWaveModel waveModel = new WaveModel(wave.Number, _delayBetweenSpawnsEnemies, enemyModels, spawnPosition, _playerGameObject.transform.position);
            waveModels.Add(waveModel);
        }
        

        IProjectileFactory projectileFactory = new ProjectileFactory(_projectileLifeCycleManager);
        IPlayerModel playerModel = new PlayerModel()
        {
            Healts = 100,
            ProjectileType = projectileModels.FirstOrDefault(x => x.Id == 1)
        };
        IPlayerView playerView = _playerGameObject.GetComponent<IPlayerView>();
        IPlayerPresenter playerPresenter = new PlayerPresenter(playerView, playerModel, projectileFactory);
        playerPresenter.Initialize();

        
        //IStatisticalItemView wavesStatisticalItemView = _wavesInfoContainer.GetComponent<IStatisticalItemView>();
        //IStatisticalItemView levelStatisticalItemView = _levelInfoContainer.GetComponent<IStatisticalItemView>();

        //IStatisticalItemView healtsStatisticalItemView = _healtsInfoContainer.GetComponent<IStatisticalItemView>();
        //IStatisticalItemModel healtsStatisticalItemModel = new StatisticalItemModel(playerModel.Healts, "Healts");
        //IStatisticalItemPresenter healtsStatisticalItemPresenter = new StatisticalItemPresenter(healtsStatisticalItemView, healtsStatisticalItemModel);
        //healtsStatisticalItemPresenter.Initialize();

        IStatisticalItemModel healtsStatisticalItemModel = InitializeStatisticalItem(_healtsStatisticalItemView, playerModel.Healts, "Healts");
        playerModel.OnModelHealtChanged += (newHealts) => healtsStatisticalItemModel.SetCount(newHealts);
        IStatisticalItemModel coinsStatisticalItemModel = InitializeStatisticalItem(_healtsStatisticalItemView, 0, "Coins");
        IStatisticalItemModel wavesStatisticalItemModel = InitializeStatisticalItem(_healtsStatisticalItemView, 1, "Waves");
        IStatisticalItemModel levelStatisticalItemModel = InitializeStatisticalItem(_healtsStatisticalItemView, 1, "Level");

        IEnemyFactory enemyFactory = new EnemyFactory(_enemyLifeCycleManager, coinsStatisticalItemModel);    
        IWaveFactory waveFactory = new WaveFactory(enemyFactory, this);
        IGameModel gameModel = new GameModel(waveModels, playerModel);
        IGameView gameView = _gameContainerGameObject.GetComponent<GameView>();
        IGamePresenter gamePresenter = new GamePresenter(gameView, gameModel, waveFactory);

        gamePresenter.Initialize();
    }


    private IStatisticalItemModel InitializeStatisticalItem(IStatisticalItemView view, int count, string name)
    {
        IStatisticalItemModel model = new StatisticalItemModel(count, name);
        IStatisticalItemPresenter presenter = new StatisticalItemPresenter(view, model);
        presenter.Initialize();
        return model;
    }

    private void OnDestroy()
    {
        
    }

    private ICollection<WaveData> GetWavesInJson()
    {
        return JsonReaderService.ReadJsonInResources<ICollection<WaveData>>("Json/Waves");
    }
    private ICollection<EnemyTypeData> GetEnemyTypesInJson()
    {
        return JsonReaderService.ReadJsonInResources<ICollection<EnemyTypeData>>("Json/EnemyTypes");
    }
    private ICollection<ProjectileTypeData> GetProjectileTypesInJson()
    {
        return JsonReaderService.ReadJsonInResources<ICollection<ProjectileTypeData>>("Json/ProjectileTypes");
    }
}
