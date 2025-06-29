
using UnityEngine;
using System.Collections.Generic;

public class ProjectileLifeCycleManager : MonoBehaviour
{
    private List<IProjectilePresenter> _activePresenters;

    private void Awake()
    {
        _activePresenters = new List<IProjectilePresenter>();
    }

    private void Update()
    {
        // ��������� ��� �������� ����������
        for (int i = _activePresenters.Count - 1; i >= 0; i--) // ���� � �������� ������� ��� ����������� ��������
        {
            _activePresenters[i].Update(Time.deltaTime);
        }
    }

    public void RegisterProjectilePresenter(IProjectilePresenter presenter)
    {
        if (!_activePresenters.Contains(presenter))
        {
            _activePresenters.Add(presenter);
        }
    }

    public void UnregisterProjectilePresenter(IProjectilePresenter presenter)
    {
        _activePresenters.Remove(presenter);
    }

    // ��� ������� ��� ���������� �����/����
    void OnDestroy()
    {
        for (int i = _activePresenters.Count - 1; i >= 0; i--) // ���� � �������� ������� ��� ����������� ��������
        {
            _activePresenters[i].Dispose();
        }
        _activePresenters.Clear();
    }
}
