using System;
using UnityEngine;

public interface IProjectileView
{
    // Методы для установки позиции и поворота
    void SetPosition(Vector2 newPosition);
    void SetRotation(Quaternion newRotation);

    // Метод для получения GameObject, чтобы Presenter мог его уничтожить
    GameObject GetGameObject();

    // Событие, о котором View сообщает Presenter'у при столкновении
    event Action<Collider2D> OnViewCollider2DTriggered;
}
