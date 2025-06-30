using UnityEngine;

public class ProjectileFactory : IProjectileFactory // ��� ������ C# ����� � ����������
{
    private ProjectileLifeCycleManager _manager; // ������ �� ��������

    public ProjectileFactory(ProjectileLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IProjectilePresenter CreateProjectile(IProjectileModel projectileModel)
    {
        float angle = Mathf.Atan2(projectileModel.Direction.y, projectileModel.Direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Projectile");
        GameObject projectile = Object.Instantiate(projectilePrefab, projectileModel.Position, quaternion);

        

        IProjectileView view = projectile.GetComponent<IProjectileView>(); // ���������, ��� ProjectileView ���������� � �������

        if (view == null)
        {
            Debug.LogError("������ ������� �� �������� ��������� ProjectileView ��� �� �� ��������� IProjectileView!");
            Object.Destroy(projectile);
            return null;
        }

        // 3. ������� Presenter, ��������� ��� View � Model, � �������� ���������� �����
        IProjectilePresenter presenter = new ProjectilePresenter(view, projectileModel, _manager);
        presenter.Initialize(); // �������������� ���������

        return presenter;
    }
    public IProjectilePresenter CreateProjectile(int id, Vector2 position, Vector2 direction, IProjectileTypeModel projectileType)
    {
        IProjectileModel model = new ProjectileModel(id, position, direction, projectileType);
        return CreateProjectile(model);
    }
}