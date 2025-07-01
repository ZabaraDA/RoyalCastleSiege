using UnityEngine;

public class StatisticalItemPresenter : IStatisticalItemPresenter
{
    private IStatisticalItemView _view; 
    private IStatisticalItemModel _model;
    
    public StatisticalItemPresenter(IStatisticalItemView view, IStatisticalItemModel model)
    {
        _model = model;
        _view = view;
    }
    public void Dispose()
    {
        _model.OnModelNameChanged -= HandleOnModelNameChanged;
        _model.OnModelCountChanged -= HandleOnModelCountChanged;
    }

    public void Initialize()
    {
        _model.OnModelNameChanged += HandleOnModelNameChanged;
        _model.OnModelCountChanged += HandleOnModelCountChanged;
        _view.SetCountText(_model.Count.ToString());
        _view.SetTypeText(_model.Name);
    }

    private void HandleOnModelNameChanged(string name)
    {
        _view.SetTypeText(name);
    }
    private void HandleOnModelCountChanged(int count)
    {
        _view.SetCountText(count.ToString());
    }
}
