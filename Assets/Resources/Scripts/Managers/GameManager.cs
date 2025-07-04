using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject _shopContainer;

    [SerializeField]
    private StatisticalItemView _levelStatisticalItemView;
    [SerializeField]
    private StatisticalItemView _coinsStatisticalItemView;
    [SerializeField]
    private StatisticalItemView _wavesStatisticalItemView;
    [SerializeField]
    private StatisticalItemView _healtsStatisticalItemView;
    [SerializeField] 
    private Image _sellectedArrowImage;



    [SerializeField]
    private float _delayBetweenSpawnsEnemies = 1.5f;

    [SerializeField]
    private IPlayerModel _playerModel;

    private void Start()
    {
        
        IStatisticalItemModel coinsStatisticalItemModel = InitializeStatisticalItem(_coinsStatisticalItemView, 0, "Coins");
        IStatisticalItemModel wavesStatisticalItemModel = InitializeStatisticalItem(_wavesStatisticalItemView, 1, "Wave");
        IStatisticalItemModel levelStatisticalItemModel = InitializeStatisticalItem(_levelStatisticalItemView, 1, "Level");

        ICollection<IEnemyTypeModel> enemyTypes = new List<IEnemyTypeModel>();
        Sprite[] enemySpritesInAtlas = Resources.LoadAll<Sprite>("Images/Enemies");
        foreach (var type in GetEnemyTypesInJson())
        {
            enemyTypes.Add(new EnemyTypeModel
            {
                Id = type.Id,
                Damage = type.Damage,
                Healt = type.Healt,
                Speed = type.Speed,
                Prize = type.Prize,
                Sprite = enemySpritesInAtlas[type.Id - 1]
            });
        }

        Debug.Log("Enemy types: " + enemyTypes.Count);
        ICollection<IProjectileTypeModel> projectileModels = new List<IProjectileTypeModel>();
        Sprite[] projectileSpritesInAtlas = Resources.LoadAll<Sprite>("Images/Arrows");
        foreach (ProjectileTypeData projectileType in GetProjectileTypesInJson().Where(x => x.Id != 5))
        {
            var projectileTypeModel = new ProjectileTypeModel(projectileType.Id, projectileType.Speed, projectileType.Damage, 100, projectileType.Cost, projectileSpritesInAtlas[projectileType.Id - 1]);
            projectileModels.Add(projectileTypeModel);

            GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Shop Item Container");
            var projectile = MonoBehaviour.Instantiate(projectilePrefab, _shopContainer.transform);
            ProductView productView = projectile.GetComponent<ProductView>();
            productView.SetCoinsStatisticalItemModel(coinsStatisticalItemModel);
            productView.SetProjectile(projectileTypeModel);
            Debug.Log("productView is null " + (productView == null));
            productView.OnClickBuyPrjectileTriggered += SetSelectedProjectile;

        }
        Debug.Log("Projectile types: " + projectileModels.Count);

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
        _playerModel = new PlayerModel()
        {
            Healts = 100,
            ProjectileType = projectileModels.FirstOrDefault(x => x.Id == 1)
        };
        SetSelectedProjectile(projectileModels.FirstOrDefault(x => x.Id == 1));
        IPlayerView playerView = _playerGameObject.GetComponent<IPlayerView>();
        IPlayerPresenter playerPresenter = new PlayerPresenter(playerView, _playerModel, projectileFactory);
        playerPresenter.Initialize();
        IStatisticalItemModel healtsStatisticalItemModel = InitializeStatisticalItem(_healtsStatisticalItemView, _playerModel.Healts, "Healts");
        _playerModel.OnModelHealtChanged += (newHealts) => healtsStatisticalItemModel.SetCount(newHealts);



        IEnemyFactory enemyFactory = new EnemyFactory(_enemyLifeCycleManager, coinsStatisticalItemModel);    
        IWaveFactory waveFactory = new WaveFactory(enemyFactory, this);
        IGameModel gameModel = new GameModel(waveModels, _playerModel);
        IGameView gameView = _gameContainerGameObject.GetComponent<GameView>();
        IGamePresenter gamePresenter = new GamePresenter(gameView, gameModel, waveFactory, wavesStatisticalItemModel);

        gamePresenter.Initialize();
    }

    private void SetSelectedProjectile(IProjectileTypeModel projectileTypeModel)
    {
        _sellectedArrowImage.sprite = projectileTypeModel.Sprite;
        _playerModel.ProjectileType = projectileTypeModel;
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
