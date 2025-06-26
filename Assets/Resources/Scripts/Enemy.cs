using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [Header("��������� �����")]
    [SerializeField] private float moveSpeed = 2f; // �������� ������������ �����
    [SerializeField] private int maxHealth = 3;    // ������������ �������� �����

    private int currentHealth;
    private GameObject player; // ������ �� ������ ������

    void Start()
    {
        currentHealth = maxHealth;
        // ������� ������ �� ���� "Player". ���������, ��� � ������ ������ ���� ���� ���.
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("������ ������ � ����� 'Player' �� ������!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // ��������� ����������� � ������
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            // ������� ����� � ������� ������
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime, Space.World);

            // ������������ ������ ����� � ������� ������ (�����������, ���� ������ ����� �����������)
            // ��� ������� ������� �� X, ���� ���� ������ �������� �����/������
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

    // ����� ��� ��������� �����
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"���� ������� {damageAmount} �����. �������� HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // ���� �������
        }
    }

    // �����, ����� ���� �������
    void Die()
    {
        Debug.Log("���� ���������!");
        // �������� ����� �������� ������, �������, ���� � �.�.
        Destroy(gameObject);
    }
}
