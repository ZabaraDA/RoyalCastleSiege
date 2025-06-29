using UnityEngine;

public class ProjectileFactory : IProjectileFactory // Или чистый C# класс с инжекциями
{
    private ProjectileLifeCycleManager _manager; // Ссылка на менеджер

    public ProjectileFactory(ProjectileLifeCycleManager manager)
    {
        _manager = manager;
    }

    public IProjectilePresenter CreateProjectile(Vector2 startPosition, Vector2 direction, IProjectileModel projectileModel)
    {
        Debug.Log("startPosition: " + startPosition.ToString());
        Debug.Log("direction: " + direction.ToString());
        // 1. Создаем модель
        ProjectileModel model = new ProjectileModel(projectileModel.Id, projectileModel.Speed, projectileModel.Damage, projectileModel.Lifetime);

        // 2. Создаем View (это единственное место, где вызывается Instantiate)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        //ToDo
        GameObject projectilePrefab = Resources.Load<GameObject>("Prefabs/Game Prefabs/Projectile");
        GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, startPosition, quaternion);


        IProjectileView view = projectile.GetComponent<IProjectileView>(); // Убедитесь, что ProjectileView прикреплен к префабу

        if (view == null)
        {
            Debug.LogError("Префаб снаряда не содержит компонент ProjectileView или он не реализует IProjectileView!");
            MonoBehaviour.Destroy(projectile);
            return null;
        }

        // 3. Создаем Presenter, передавая ему View и Model, и менеджер жизненного цикла
        IProjectilePresenter presenter = new ProjectilePresenter(view, model, _manager);
        presenter.Initialize(); // Инициализируем презентер

        return presenter;
    }
}