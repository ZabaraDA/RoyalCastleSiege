using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModel : IGameModel
{
    public ICollection<IWaveModel> Waves { get; set; }
    public IPlayerModel Player { get; set; }
    public IStatisticalItemModel CoinsStatisticalItemModel { get; set; }

    private IWaveModel _currentWave;
    public IWaveModel CurrentWave 
    { 
        get
        {
            return _currentWave;
        }
        set
        {
            if (_currentWave != value)
            {
                _currentWave = value;
                OnModelCurrentWaveChanged?.Invoke(_currentWave);
                Debug.Log($"Field '{nameof(CurrentWave)}' changed in model");
            }
        } 
    }

    public GameModel(ICollection<IWaveModel> waves, IPlayerModel playerModel) 
    {
        Waves = waves;
        Player = playerModel;
        CurrentWave = Waves.FirstOrDefault(x => x.Number == 1);
    }

    public event Action<IWaveModel> OnModelCurrentWaveChanged;
}
