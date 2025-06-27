using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    ICollection<IWaveModel> Waves { get; set; }
    IPlayerModel Player { get; set; }

}
