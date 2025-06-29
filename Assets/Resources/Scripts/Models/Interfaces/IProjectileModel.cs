using UnityEngine;

public interface IProjectileModel
{
    int Id { get; set; }
    int Damage { get; set; }
    int Cost { get; set; }
    Vector2 Position { get; } // ������� �������
    Vector2 Direction { get; } // ����������� ��������
    float Speed { get; }     // �������� ��������
    float Lifetime { get; }  // ������������ ����� �����
    bool IsAlive { get; }    // ���� �� ������

    void UpdatePosition(float deltaTime);
}
