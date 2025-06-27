using UnityEngine;

public class ProjectileView : MonoBehaviour, IProjectileView
{
    private Vector2 moveDirection; // ����������� �������� �������

    void Start()
    {
    }

    void Update()
    {
        // ������� ������ � �������� �����������
        // transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // ����� ��� ��������� ����������� �������� �������
    public void SetDirection(Quaternion quaternion)
    {
        transform.rotation = quaternion;
        
    }

    // ����������� ������������ � ���������� (��������, � �������)
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, �������� �� ������, � ������� �����������, ������
        if (other.CompareTag("Enemy"))
        {
            EnemyView enemyView = other.GetComponent<EnemyView>();
            if (enemyView != null)
            {
                //enemyView.TakeDamage(damage); // ������� ���� �����
            }
            Destroy(gameObject); // ���������� ������ ����� ������������
        }
    }
}
