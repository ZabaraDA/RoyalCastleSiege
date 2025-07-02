using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePresenter : IWavePresenter
{
    private IWaveView _view;
    private IWaveModel _model;
    private IEnemyFactory _enemyFactory;
    private MonoBehaviour _coroutineRunner;
    private ICollection<IEnemyPresenter> _enemyPresenterList;

    public event Action<IWaveModel> OnPresenterWaveCompleted;

    // �������� �����������, ����� ��������� MonoBehaviour ��� ������� �������
    public WavePresenter(IWaveView view, IWaveModel model, IEnemyFactory enemyFactory, MonoBehaviour coroutineRunner)
    {
        _view = view;
        _model = model;
        _enemyFactory = enemyFactory;
        _coroutineRunner = coroutineRunner; // ��������� ������ �� ������, ������� ����� ��������� ��������
        _enemyPresenterList = new List<IEnemyPresenter>();
    }

    public void Dispose()
    {
        // ������ �������, ���� Presenter ������������ �� �������
    }

    public void Initialize()
    {
        StartWave();
    }

    // ���� ����� ������ ����� ��������� �������� ������
    public void StartWave()
    {
        if (_coroutineRunner == null)
        {
            Debug.LogError("Coroutine Runner (MonoBehaviour) �� ������������ WavePresenter'�. ���������� ��������� ����� ������.");
            return;
        }
        // ��������� �������� ������ ����� ��������������� MonoBehaviour
        _coroutineRunner.StartCoroutine(SpawnEnemiesWithDelayCoroutine());
    }

    public void EndWave()
    {
        Dispose();
        OnPresenterWaveCompleted?.Invoke(_model);
    }
    private void HandleOnPresenterEnemyDestoyed(IEnemyPresenter enemyPresenter)
    {
        _enemyPresenterList.Remove(enemyPresenter);
        if (_enemyPresenterList.Count < 1)
        {
            EndWave();
        }
    }
    private IEnumerator SpawnEnemiesWithDelayCoroutine()
    {

        foreach (var enemyCountEntry in _model.EnemiesCount)
        {
            for (var i = 1; i <= enemyCountEntry.Count; i++)
            {
                // �������� �� null ��� SpawnPositions
                if (_model.SpawnPositions == null || _model.SpawnPositions.Count == 0)
                {
                    Debug.LogError("����� ������ �� ��������� ��� �����. ���������� �������� ������.");
                    yield break; // ��������� ��������
                }

                // �������� ��������� ������� ������ �� ���������
                Vector2 spawnPosition = _model.SpawnPositions[UnityEngine.Random.Range(0, _model.SpawnPositions.Count)];

                // ������� �����
                var enemyPresenter = _enemyFactory.CreateEnemy(
                    i, // ����� ����� (���� ����� ��� ������������)
                    spawnPosition,
                    _model.TargetPosition, // ������� ������� (���� ����� � ��� ��������)
                    enemyCountEntry.EnemyType
                );
                _enemyPresenterList.Add(enemyPresenter);

                enemyPresenter.OnPresenterEnemyPresenterDestoyed += HandleOnPresenterEnemyDestoyed;

                Debug.Log($"����� ����� ���� {enemyCountEntry.EnemyType.Id} � ������� {spawnPosition}.");

                // ���� �������� ����� ����� ������� ���������� �����
                yield return new WaitForSeconds(_model.DelayBetweenSpawnsEnemies);
            }
        }

        Debug.Log("��� ����� � ����� �������.");
    }
}
