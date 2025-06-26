using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("��������� �������")]
    [SerializeField] private float speed = 10f; // �������� �������
    [SerializeField] private int damage = 1;    // ����, ��������� ��������
    [SerializeField] private float lifetime = 3f; // ����� ����� ������� �� ���������������

    private Vector2 moveDirection; // ����������� �������� �������

    void Start()
    {
        // ���������� ������ ����� �������� �����, ����� �� �� ����� ����������
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // ������� ������ � �������� �����������
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // ����� ��� ��������� ����������� �������� �������
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // �����������, ����� �������� ���� ����������
        // ������������� ������� ������� � ������� ��������
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    // ����������� ������������ � ���������� (��������, � �������)
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, �������� �� ������, � ������� �����������, ������
        if (other.CompareTag("Enemy"))
        {
            EnemyView enemy = other.GetComponent<EnemyView>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // ������� ���� �����
            }
            Destroy(gameObject); // ���������� ������ ����� ������������
        }
    }
}
