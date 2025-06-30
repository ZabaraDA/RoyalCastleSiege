using UnityEngine;

public interface IGamePresenter : IInitializable, IDisposable
{
    void LoadWave(IWaveModel waveModel);
}
