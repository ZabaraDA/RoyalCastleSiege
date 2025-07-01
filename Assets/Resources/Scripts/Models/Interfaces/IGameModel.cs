using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    ICollection<IWaveModel> Waves { get; set; }
    IWaveModel CurrentWave { get; set; }
    IPlayerModel Player { get; set; }
    IStatisticalItemModel CoinsStatisticalItemModel { get; set; }

    event Action<IWaveModel> OnModelCurrentWaveChanged;

}
