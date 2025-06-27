using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private GameObject projectilePrefab; // ������ �������
    [SerializeField] private Transform firePoint;         // �����, �� ������� �������� ������
    [SerializeField] private float fireRate = 0.5f;       // �������� �������� (�������� � �������)

    [Header("��������� ����")]
    [SerializeField] private Transform bowTransform;      // ������ �� Transform ��������� ������� ����
    // playerRotationOffset ������ �� �����, �.�. �������� �� ���������, � ������ ���������
    [SerializeField] private float bowRotationOffset = 0f;    // �������� ��� �������� ���� (���� ������ �� ��������� ������� �� ������)


    private float nextFireTime; // �����, ����� ����� ����� �������� �����
    private SpriteRenderer playerSpriteRenderer; // ��� �������� ������� ������ (flipX)
    private SpriteRenderer bowSpriteRenderer;    // ��� �������� ������� ���� (flipY)

    void Awake()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer �� ������ �� ���������. �������������� ������� (flipX) ������ �� ����� ��������.");
        }

        if (bowTransform != null)
        {
            bowSpriteRenderer = bowTransform.GetComponent<SpriteRenderer>();
            if (bowSpriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer �� ������ �� ������� ����. �������������� ������� ������� ���� �� ����� ��������.");
            }
        }
        else
        {
            Debug.LogError("�� ��������� ������ �� Transform ���� (bowTransform) � ����������! ������ ���� �� ����� ��������.");
        }
        if (firePoint == null)
        {
            Debug.LogError("�� ��������� ������ �� Transform FirePoint � ����������! ������� �� ����� ��������.");
        }
    }

    void Update()
    {
        // ����������� �������� ����/������ ��� ���������� ������� � �������� ���� � �������� �������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ��� 2D, ����������, ��� Z-���������� ����� 0

        HandlePlayerAndBowOrientation(mousePosition);

        // ���������, ��� �� ���� ���� ��� ������� ������
        if (Input.GetMouseButtonDown(0)) // 0 - ����� ������ ���� / ������ �������
        {
            // ���������, ����� �� �� ��������
            if (Time.time >= nextFireTime)
            {
                Shoot(mousePosition);
                nextFireTime = Time.time + 1f / fireRate; // ��������� ����� ��������� ��������
            }
        }
    }

    // ����� ��� ���������� ���������� ������ � ����
    void HandlePlayerAndBowOrientation(Vector3 targetPosition)
    {
        // ��������� ����������� �� ��������� �� ���� (�������)
        Vector2 directionToTarget = (targetPosition - transform.position).normalized;

        //// --- ������� ������ (������ FLIPX) ---
        //if (playerSpriteRenderer != null)
        //{
        //    bool targetIsRight = targetPosition.x > transform.position.x;
        //    playerSpriteRenderer.flipX = !targetIsRight; // true ���� ���� �����, false ���� ���� ������
        //}

        // --- ������� ���� ---
        if (bowTransform != null)
        {
            // ��������� ������� ����, ���� ������ �������� ���
            float bowWorldAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // ������������ ������ ���� (��� flipY), ���� �������� ���������� (flipX)
            if (bowSpriteRenderer != null)
            {
                // ���� �������� ������� ����� (playerSpriteRenderer.flipX = true),
                // �� ��� ������ ���� ���������� �� ��� Y, ����� ����� ���������� � ���������.
                // ��� ������������� ��, ��� ��� ����� ��������� "������������ ����� ������",
                // ����� �������� ������ �������.
                bowSpriteRenderer.flipY = playerSpriteRenderer != null && playerSpriteRenderer.flipX;
            }

            // ������� ��� ������ ����
            // ������������ ����, ���� ������ ���� ��������� ���������� �� Y
            float finalBowAngle = bowWorldAngle;
            if (bowSpriteRenderer != null && bowSpriteRenderer.flipY)
            {
                finalBowAngle = -bowWorldAngle; // ������������ flipY, ����� ��� ��������� �������
            }
            finalBowAngle += bowRotationOffset; // ��������� �������� ��� ������� ����

            // ������������� ������� ������� ����
            bowTransform.rotation = Quaternion.Euler(0, 0, finalBowAngle);
        }
    }
}