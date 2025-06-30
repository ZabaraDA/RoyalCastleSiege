using System.Linq;
using UnityEngine;

public class GamePresenter : IGamePresenter
{
    private IGameView _view;
    private IGameModel _model;
    private IWaveFactory _waveFactory;
    public GamePresenter(IGameView view, IGameModel model, IWaveFactory waveFactory)
    {
        _view = view;
        _model = model;
        _waveFactory = waveFactory;
    }

    public void Dispose()
    {
        _model.OnModelCurrentWaveChanged -= LoadWave;
    }

    public void Initialize()
    {
        _model.OnModelCurrentWaveChanged += LoadWave;
        LoadWave(_model.CurrentWave);
        Debug.Log("_model.CurrentWave is null: " + _model.CurrentWave == null);
    }

    public void LoadWave(IWaveModel waveModel)
    {
        Debug.Log("LoadWave " + waveModel.Number);
        IWavePresenter wavePresenter = _waveFactory.CreateWave(waveModel);
    }
}
