using System;
using UnityEngine;

public interface IStatisticalItemModel
{
    int Count { get; set; }
    string Name { get; set; }

    event Action<int> OnModelCountChanged;
    event Action<string> OnModelNameChanged;
}
