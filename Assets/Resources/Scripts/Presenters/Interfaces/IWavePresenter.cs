using System;
using UnityEngine;

public interface IWavePresenter : IInitializable, IDisposable
{
    void StartWave();
    event Action<IWaveModel> OnPresenterWaveCompleted;
}
