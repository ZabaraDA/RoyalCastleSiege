using System.Collections.Generic;
using UnityEngine;

public interface IWaveModel
{
    ICollection<IEnemyModel> Enemies { get; set; }
}
