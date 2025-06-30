using UnityEngine;

public interface IWaveFactory
{
    //IWavePresenter CreateWave(int id);
    IWavePresenter CreateWave(IWaveModel waveModel);
}
