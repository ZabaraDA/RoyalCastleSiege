using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [Header("Настройки врага")]
    [SerializeField] private float moveSpeed = 2f; // Скорость передвижения врага
    [SerializeField] private int maxHealth = 3;    // Максимальное здоровье врага

    private int currentHealth;
    private GameObject player; // Ссылка на объект игрока

    void Start()
    {
        currentHealth = maxHealth;
        // Находим игрока по тегу "Player". Убедитесь, что у вашего игрока есть этот тег.
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Объект игрока с тегом 'Player' не найден!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Вычисляем направление к игроку
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            // Двигаем врага в сторону игрока
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime, Space.World);

            // Поворачиваем спрайт врага в сторону игрока (опционально, если спрайт имеет направление)
            // Это простой поворот по X, если враг должен смотреть влево/вправо
            if (directionToPlayer.x > 0 && GetComponent<SpriteRenderer>() != null && GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (directionToPlayer.x < 0 && GetComponent<SpriteRenderer>() != null && !GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    // Метод для получения урона
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Враг получил {damageAmount} урона. Осталось HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // Враг умирает
        }
    }

    // Метод, когда враг умирает
    void Die()
    {
        Debug.Log("Враг уничтожен!");
        // Добавьте здесь анимацию смерти, эффекты, звук и т.д.
        Destroy(gameObject);
    }
}
