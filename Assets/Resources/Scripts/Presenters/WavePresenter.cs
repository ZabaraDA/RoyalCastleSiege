using System;
using System.Collections;
using UnityEngine;

public class WavePresenter : IWavePresenter
{
    private IWaveView _view;
    private IWaveModel _model;
    private IEnemyFactory _enemyFactory;
    private MonoBehaviour _coroutineRunner; // <<< НОВОЕ ПОЛЕ: Для запуска корутин

    public event Action<IWaveModel> OnPresenterWaveCompleted;

    // Изменяем конструктор, чтобы принимать MonoBehaviour для запуска корутин
    public WavePresenter(IWaveView view, IWaveModel model, IEnemyFactory enemyFactory, MonoBehaviour coroutineRunner)
    {
        _view = view;
        _model = model;
        _enemyFactory = enemyFactory;
        _coroutineRunner = coroutineRunner; // Сохраняем ссылку на объект, который может запускать корутины
    }

    public void Dispose()
    {
        // Логика очистки, если Presenter подписывался на события
    }

    public void Initialize()
    {
        StartWave();
    }

    // Этот метод теперь будет запускать корутину спавна
    public void StartWave() // Переименовал Start() в StartWave() для ясности
    {
        if (_coroutineRunner == null)
        {
            Debug.LogError("Coroutine Runner (MonoBehaviour) не предоставлен WavePresenter'у. Невозможно запустить спавн врагов.");
            return;
        }
        // Запускаем корутину спавна через предоставленный MonoBehaviour
        _coroutineRunner.StartCoroutine(SpawnEnemiesWithDelayCoroutine());
    }

    private IEnumerator SpawnEnemiesWithDelayCoroutine()
    {

        foreach (var enemyCountEntry in _model.EnemiesCount)
        {
            for (var i = 1; i <= enemyCountEntry.Count; i++)
            {
                // Проверки на null для SpawnPositions
                if (_model.SpawnPositions == null || _model.SpawnPositions.Count == 0)
                {
                    Debug.LogError("Точки спавна не настроены для волны. Невозможно спавнить врагов.");
                    yield break; // Прерываем корутину
                }

                // Выбираем случайную позицию спавна из доступных
                Vector2 spawnPosition = _model.SpawnPositions[UnityEngine.Random.Range(0, _model.SpawnPositions.Count)];

                // Создаем врага
                _enemyFactory.CreateEnemy(
                    i, // Номер врага (если нужен для уникальности)
                    spawnPosition,
                    _model.TargetPosition, // Целевая позиция (если враги к ней движутся)
                    enemyCountEntry.EnemyType
                );

                Debug.Log($"Спавн врага типа {enemyCountEntry.EnemyType.Id} в позиции {spawnPosition}.");

                // Ждем заданное время перед спавном следующего врага
                yield return new WaitForSeconds(_model.DelayBetweenSpawnsEnemies);
            }
        }

        Debug.Log("Все враги в волне созданы.");
    }
}
