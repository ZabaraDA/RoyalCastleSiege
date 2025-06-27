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
        
    }

    public void Initialize()
    {
        
    }
}
