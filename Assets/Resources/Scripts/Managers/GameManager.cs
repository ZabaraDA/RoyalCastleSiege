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
            IWaveModel waveModel = new WaveModel(wave.Number, enemyModels, spawnPosition, _playerGameObject.transform.position);
            waveModels.Add(waveModel);
        }
        

        IProjectileFactory projectileFactory = new ProjectileFactory(_projectileLifeCycleManager);
        IPlayerModel playerModel = new PlayerModel()
        {
            Healt = 100,
            ProjectileType = projectileModels.FirstOrDefault(x => x.Id == 1)
        };
        IPlayerView playerView = _playerGameObject.GetComponent<IPlayerView>();
        IPlayerPresenter playerPresenter = new PlayerPresenter(playerView, playerModel, projectileFactory);
        playerPresenter.Initialize();

        IEnemyFactory enemyFactory = new EnemyFactory(_enemyLifeCycleManager);    
        IWaveFactory waveFactory = new WaveFactory(enemyFactory);
        IGameModel gameModel = new GameModel(waveModels, playerModel);
        IGameView gameView = _gameContainerGameObject.GetComponent<GameView>();
        IGamePresenter gamePresenter = new GamePresenter(gameView, gameModel, waveFactory);

        gamePresenter.Initialize();
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
