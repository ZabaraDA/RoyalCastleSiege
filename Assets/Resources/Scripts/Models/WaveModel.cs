using System.Collections.Generic;
using UnityEngine;

public class WaveModel : IWaveModel
{
    public ICollection<IEnemyModel> Enemies { get; set; }
    public WaveModel(ICollection<IEnemyModel> enemies)
    {
        Enemies = enemies;
    }

}
