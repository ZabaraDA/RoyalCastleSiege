using UnityEngine;

public class ProjectileFactory : IProjectileFactory // ��� ������ C# ����� � ����������
{
    private ProjectileLifeCycleManager _manager; // ������ �� ��������

    public ProjectileFactory(ProjectileLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IProjectilePresenter CreateProjectile(Vector2 startPosition, Vector2 direction, IProjectileModel projectileModel)
    {
        Debug.Log("startPosition: " + startPosition.ToString());
        Debug.Log("direction: " + direction.ToString());
        // 1. ������� ������
        ProjectileModel model = new ProjectileModel(projectileModel.Id, projectileModel.Speed, projectileModel.Damage, projectileModel.Lifetime);

        // 2. ������� View (��� ������������ �����, ��� ���������� Instantiate)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        //ToDo
        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Projectile");
        GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, startPosition, quaternion);


        IProjectileView view = projectile.GetComponent<IProjectileView>(); // ���������, ��� ProjectileView ���������� � �������

        if (view == null)
        {
            Debug.LogError("������ ������� �� �������� ��������� ProjectileView ��� �� �� ��������� IProjectileView!");
            MonoBehaviour.Destroy(projectile);
            return null;
        }

        // 3. ������� Presenter, ��������� ��� View � Model, � �������� ���������� �����
        IProjectilePresenter presenter = new ProjectilePresenter(view, model, _manager);
        presenter.Initialize(); // �������������� ���������

        return presenter;
    }
}