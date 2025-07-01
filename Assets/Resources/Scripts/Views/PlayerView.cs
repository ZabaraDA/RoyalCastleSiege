using System;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{

    public event Action<Vector3,Vector3> OnViewMouseButtonClick;
    public event Action<int> OnViewTakeDamageTriggered;

    [SerializeField]
    private GameObject _firePointGameObject;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Debug.Log("PlayerView position: " + mousePosition);
            OnViewMouseButtonClick?.Invoke(mousePosition, _firePointGameObject.transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        OnViewTakeDamageTriggered?.Invoke(damage);
    }
}
