using System;
using UnityEngine;

public class ProjectileView : MonoBehaviour, IProjectileView
{
    public event Action<Collider2D> OnViewCollider2DTriggered;

    void Update()
    {

    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnViewCollider2DTriggered?.Invoke(other);
    }
}
