using System;
using UnityEngine;

public interface IPlayerView
{
    event Action<Vector3, Vector3> OnViewMouseButtonClick;
    event Action<int> OnViewTakeDamageTriggered;
    void TakeDamage(int damage);
}
