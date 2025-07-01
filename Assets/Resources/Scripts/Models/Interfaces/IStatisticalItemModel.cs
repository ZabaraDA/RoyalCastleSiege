using System;
using UnityEngine;

public interface IStatisticalItemModel
{
    int Count { get; set; }
    string Name { get; set; }

    void SetName(string name);
    void SetCount(int count);
    void AddCount(int count);

    event Action<int> OnModelCountChanged;
    event Action<string> OnModelNameChanged;
}
