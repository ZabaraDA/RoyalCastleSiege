using UnityEngine;

public class ProjectileView : MonoBehaviour, IProjectileView
{
    private Vector2 moveDirection; // Направление движения снаряда

    void Start()
    {
    }

    void Update()
    {
        // Двигаем снаряд в заданном направлении
        // transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // Метод для установки направления движения снаряда
    public void SetDirection(Quaternion quaternion)
    {
        transform.rotation = quaternion;
        
    }

    // Обнаружение столкновений с триггерами (например, с врагами)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект, с которым столкнулись, врагом
        if (other.CompareTag("Enemy"))
        {
            EnemyView enemyView = other.GetComponent<EnemyView>();
            if (enemyView != null)
            {
                //enemyView.TakeDamage(damage); // Наносим урон врагу
            }
            Destroy(gameObject); // Уничтожаем снаряд после столкновения
        }
    }
}
