using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePresenter : IGamePresenter
{
    private IGameView _view;
    private IGameModel _model;
    private IWaveFactory _waveFactory;
    private IWavePresenter _wavePresenter;
    IStatisticalItemModel _wavesStatisticalItemModel;
    public GamePresenter(IGameView view, IGameModel model, IWaveFactory waveFactory, IStatisticalItemModel wavesStatisticalItemModel)
    {
        _view = view;
        _model = model;
        _waveFactory = waveFactory;
        _wavesStatisticalItemModel = wavesStatisticalItemModel;
    }

    public void Dispose()
    {
        _model.OnModelCurrentWaveChanged -= LoadWave;
        _model.OnModelCurrentWaveChanged -= (model) => _wavesStatisticalItemModel.SetCount(model.Number);
    }

    public void Initialize()
    {
        _model.OnModelCurrentWaveChanged += (model) => _wavesStatisticalItemModel.SetCount(model.Number);
        _model.OnModelCurrentWaveChanged += LoadWave;
        LoadWave(_model.CurrentWave);
        Debug.Log("_model.CurrentWave is null: " + _model.CurrentWave == null);
    }

    public void LoadWave(IWaveModel waveModel)
    {
        Debug.Log("LoadWave " + waveModel.Number);
        _wavePresenter = _waveFactory.CreateWave(waveModel);
        _wavePresenter.OnPresenterWaveCompleted += HandleOnPresenterWaveCompleted;
    }

    private void LoadNextWave(IWaveModel currentModel)
    {
        _model.CurrentWave = _model.Waves.FirstOrDefault(x => x.Number == (currentModel.Number + 1));
        //if (wave != null)
        //{
        //    _model.CurrentWave = wave;
        //}
        //else
        //{
        //    SceneManager.LoadScene(0);
        //}
    }
    private void HandleOnPresenterWaveCompleted(IWaveModel waveModel)
    {
        _wavePresenter.OnPresenterWaveCompleted -= HandleOnPresenterWaveCompleted;
        _wavePresenter.Dispose();
        LoadNextWave(waveModel);
    }
}
