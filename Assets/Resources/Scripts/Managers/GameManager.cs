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
    private ProjectileLifeCycleManager _manager; // —сылка на менеджер



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

        ICollection<IProjectileModel> projectileModels = new List<IProjectileModel>();
        foreach (ProjectileType projectileType in GetProjectileTypesInJson())
        {
            projectileModels.Add(new ProjectileModel(projectileType.Id, projectileType.Speed, projectileType.Damage, 100));
        }

        

        ICollection<IWaveModel> waveModels = new List<IWaveModel>();

        foreach (var wave in GetWavesInJson())
        {
            ICollection<IEnemyModel> enemyModels = new List<IEnemyModel>();
            foreach (var enemyCount in wave.EnemyCountList)
            {
                IEnemyTypeModel type = enemyTypes.FirstOrDefault(x => x.Id == enemyCount.EnemyId);
                for (int i = 1; i <= enemyCount.Count; i++)
                {
                    IEnemyModel enemyModel = new EnemyModel()
                    {
                        Healt = type.Healt,
                        Type = type,
                        Number = i,
                    };
                    enemyModels.Add(enemyModel);
                }
            }
            IWaveModel waveModel = new WaveModel(enemyModels);
            waveModels.Add(waveModel);
        }
        IProjectileFactory projectileFactory = new ProjectileFactory(_manager);

        IPlayerModel playerModel = new PlayerModel()
        {
            Healt = 100,
            Projectile = projectileModels.FirstOrDefault(x => x.Id == 1)
        };
        IPlayerView playerView = _playerGameObject.GetComponent<IPlayerView>();
        IPlayerPresenter playerPresenter = new PlayerPresenter(playerView, playerModel, projectileFactory);
        playerPresenter.Initialize();

        IGameModel gameModel = new GameModel(waveModels, playerModel);
        IGameView gameView = _gameContainerGameObject.GetComponent<GameView>();
        IGamePresenter gamePresenter = new GamePresenter(gameView, gameModel);

        gamePresenter.Initialize();
    }

    private ICollection<Wave> GetWavesInJson()
    {
        return JsonReaderService.ReadJsonInResources<ICollection<Wave>>("Json/Waves");
    }
    private ICollection<EnemyType> GetEnemyTypesInJson()
    {
        return JsonReaderService.ReadJsonInResources<ICollection<EnemyType>>("Json/EnemyTypes");
    }
    private ICollection<ProjectileType> GetProjectileTypesInJson()
    {
        return JsonReaderService.ReadJsonInResources<ICollection<ProjectileType>>("Json/EnemyTypes");
    }
}
