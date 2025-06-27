using System.Collections.Generic;
using UnityEngine;

public class GameModel : IGameModel
{
    public ICollection<IWaveModel> Waves { get; set; }
    public IPlayerModel Player { get; set; }

    public GameModel(ICollection<IWaveModel> waves, IPlayerModel playerModel) 
    {
        Waves = waves;
        Player = playerModel;
    }
}
