using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeCycleManager : MonoBehaviour
{
    private List<IEnemyPresenter> _activePresenters;

    private void Awake()
    {
        _activePresenters = new List<IEnemyPresenter>();
    }

    private void Update()
    {
        for (int i = _activePresenters.Count - 1; i >= 0; i--)
        {
            _activePresenters[i].Update(Time.deltaTime);
        }
    }

    public void RegisterEnemyPresenter(IEnemyPresenter presenter)
    {
        if (!_activePresenters.Contains(presenter))
        {
            _activePresenters.Add(presenter);
        }
    }

    public void UnregisterEnemyPresenter(IEnemyPresenter presenter)
    {
        _activePresenters.Remove(presenter);
    }

    void OnDestroy()
    {
        for (int i = _activePresenters.Count - 1; i >= 0; i--)
        {
            _activePresenters[i].Dispose();
        }
        _activePresenters.Clear();
    }
}
