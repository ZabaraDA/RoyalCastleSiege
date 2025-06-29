using System;
using UnityEngine;

public interface IProjectileView
{
    // ������ ��� ��������� ������� � ��������
    void SetPosition(Vector2 newPosition);
    void SetRotation(Quaternion newRotation);

    // ����� ��� ��������� GameObject, ����� Presenter ��� ��� ����������
    GameObject GetGameObject();

    // �������, � ������� View �������� Presenter'� ��� ������������
    event Action<Collider2D> OnViewCollider2DTriggered;
}
