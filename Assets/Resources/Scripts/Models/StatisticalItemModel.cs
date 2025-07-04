using System;
using UnityEngine;

public class StatisticalItemModel : IStatisticalItemModel
{
    private int _count;
    public int Count
    {
        get => _count;
        set
        {
            if (_count != value)
            {
                _count = value;
                OnModelCountChanged?.Invoke(_count);
                Debug.Log($"Field '{nameof(Count)}' changed in model");
            }
        }
    }

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnModelNameChanged?.Invoke(_name);
                Debug.Log($"Field '{nameof(Name)}' changed in model");
            }
        }
    }

    public event Action<int> OnModelCountChanged;
    public event Action<string> OnModelNameChanged;
    
    public StatisticalItemModel( int count, string name)
    {
        Count = count;
        Name = name;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetCount(int count)
    {
       Debug.Log("SetCount: " + count);
       Count = count;
    }

    public void AddCount(int count)
    {
        Count += count;
    }
}
