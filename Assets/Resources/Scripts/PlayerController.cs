using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    [SerializeField] private GameObject projectilePrefab; // Префаб снаряда
    [SerializeField] private Transform firePoint;         // Точка, из которой вылетает снаряд
    [SerializeField] private float fireRate = 0.5f;       // Скорость стрельбы (снарядов в секунду)

    [Header("Настройки лука")]
    [SerializeField] private Transform bowTransform;      // Ссылка на Transform дочернего объекта лука
    // playerRotationOffset больше не нужен, т.к. персонаж не вращается, а только флипается
    [SerializeField] private float bowRotationOffset = 0f;    // Смещение для поворота лука (если спрайт по умолчанию смотрит не вправо)


    private float nextFireTime; // Время, когда можно будет стрелять снова
    private SpriteRenderer playerSpriteRenderer; // Для поворота спрайта игрока (flipX)
    private SpriteRenderer bowSpriteRenderer;    // Для поворота спрайта лука (flipY)

    void Awake()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer не найден на персонаже. Автоматический поворот (flipX) игрока не будет работать.");
        }

        if (bowTransform != null)
        {
            bowSpriteRenderer = bowTransform.GetComponent<SpriteRenderer>();
            if (bowSpriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer не найден на объекте лука. Автоматический поворот спрайта лука не будет работать.");
            }
        }
        else
        {
            Debug.LogError("Не назначена ссылка на Transform лука (bowTransform) в инспекторе! Орбита лука не будет работать.");
        }
        if (firePoint == null)
        {
            Debug.LogError("Не назначена ссылка на Transform FirePoint в инспекторе! Снаряды не будут вылетать.");
        }
    }

    void Update()
    {
        // Отслеживаем движение мыши/пальца для обновления позиции и поворота лука в реальном времени
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Для 2D, убеждаемся, что Z-координата равна 0

        HandlePlayerAndBowOrientation(mousePosition);

        // Проверяем, был ли клик мыши или касание экрана
        if (Input.GetMouseButtonDown(0)) // 0 - левая кнопка мыши / первое касание
        {
            // Проверяем, можем ли мы стрелять
            if (Time.time >= nextFireTime)
            {
                Shoot(mousePosition);
                nextFireTime = Time.time + 1f / fireRate; // Обновляем время следующей стрельбы
            }
        }
    }

    // Метод для обновления ориентации игрока и лука
    void HandlePlayerAndBowOrientation(Vector3 targetPosition)
    {
        // Вычисляем направление от персонажа до цели (курсора)
        Vector2 directionToTarget = (targetPosition - transform.position).normalized;

        //// --- ПОВОРОТ ИГРОКА (ТОЛЬКО FLIPX) ---
        //if (playerSpriteRenderer != null)
        //{
        //    bool targetIsRight = targetPosition.x > transform.position.x;
        //    playerSpriteRenderer.flipX = !targetIsRight; // true если цель слева, false если цель справа
        //}

        // --- ПОВОРОТ ЛУКА ---
        if (bowTransform != null)
        {
            // Вычисляем мировой угол, куда должен смотреть лук
            float bowWorldAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Поворачиваем спрайт лука (его flipY), если персонаж перевернут (flipX)
            if (bowSpriteRenderer != null)
            {
                // Если персонаж смотрит влево (playerSpriteRenderer.flipX = true),
                // то лук должен быть перевернут по оси Y, чтобы ручка оставалась к персонажу.
                // Это предотвращает то, что лук будет выглядеть "перевернутым вверх ногами",
                // когда персонаж меняет сторону.
                bowSpriteRenderer.flipY = playerSpriteRenderer != null && playerSpriteRenderer.flipX;
            }

            // Вращаем сам объект лука
            // Корректируем угол, если спрайт лука визуально перевернут по Y
            float finalBowAngle = bowWorldAngle;
            if (bowSpriteRenderer != null && bowSpriteRenderer.flipY)
            {
                finalBowAngle = -bowWorldAngle; // Компенсируем flipY, чтобы лук правильно смотрел
            }
            finalBowAngle += bowRotationOffset; // Применяем смещение для спрайта лука

            // Устанавливаем мировой поворот лука
            bowTransform.rotation = Quaternion.Euler(0, 0, finalBowAngle);
        }
    }
}