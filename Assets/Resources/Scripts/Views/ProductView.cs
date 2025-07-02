using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    [SerializeField]
    private Button _buyButton;
    [SerializeField]
    private TMP_Text _costText;
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private int _cost;
    [SerializeField]
    private Sprite _sprite;

    private void Start()
    {
        _costText.text = _cost.ToString();
    }
}
