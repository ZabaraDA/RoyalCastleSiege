using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Настройки снаряда")]
    [SerializeField] private float speed = 10f; // Скорость снаряда
    [SerializeField] private int damage = 1;    // Урон, наносимый снарядом
    [SerializeField] private float lifetime = 3f; // Время жизни снаряда до самоуничтожения

    private Vector2 moveDirection; // Направление движения снаряда

    void Start()
    {
        // Уничтожаем снаряд через заданное время, чтобы он не летел бесконечно
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Двигаем снаряд в заданном направлении
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // Метод для установки направления движения снаряда
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // Нормализуем, чтобы скорость была постоянной
        // Устанавливаем поворот снаряда в сторону движения
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    // Обнаружение столкновений с триггерами (например, с врагами)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект, с которым столкнулись, врагом
        if (other.CompareTag("Enemy"))
        {
            EnemyView enemy = other.GetComponent<EnemyView>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Наносим урон врагу
            }
            Destroy(gameObject); // Уничтожаем снаряд после столкновения
        }
    }
}
