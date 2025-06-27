using UnityEngine;

public class WavePresenter : IWavePresenter
{
    private IWaveView _view;
    private IWaveModel _model;
    public WavePresenter(IWaveView view, IWaveModel model)
    {
        _view = view;
        _model = model;
    }

    public void Dispose()
    {
        
    }

    public void Initialize()
    {
        
    }
}
