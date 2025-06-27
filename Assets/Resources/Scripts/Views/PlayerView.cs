using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerView : MonoBehaviour, IPlayerView
{

    public event Action<Vector3,Vector3> OnViewMouseButtonClick;
    [SerializeField]
    private GameObject _firePointGameObject;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            OnViewMouseButtonClick?.Invoke(mousePosition, _firePointGameObject.transform.position);
        }
    }
}
