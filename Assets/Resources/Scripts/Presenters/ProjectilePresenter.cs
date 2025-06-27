using UnityEngine;

public class ProjectilePresenter : IProjectilePresenter
{
    private IProjectileView _projectileView;
    private IProjectileModel _projectileModel;
    public ProjectilePresenter(IProjectileView projectileView, IProjectileModel projectileModel)
    {
        _projectileModel = projectileModel;
        _projectileView = projectileView;
    }
    public void Dispose()
    {
        
    }

    public void Initialize()
    {
        
    }
}
