using System;
using UnityEngine;

public interface IWavePresenter : IInitializable, IDisposable
{
    void Start();
    event Action<IWaveModel> OnPresenterWaveCompleted;
}
