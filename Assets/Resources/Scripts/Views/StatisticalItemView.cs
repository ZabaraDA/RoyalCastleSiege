using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticalItemView : MonoBehaviour, IStatisticalItemView 
{
    [SerializeField]
    private TMP_Text _typeText;
    [SerializeField]
    private TMP_Text _countText;
    [SerializeField]
    private Image _typeImage;


    public void UpdateDisplay(string typeText, string countText, Sprite typeImage = null)
    {
        if (_typeText != null) _typeText.text = typeText;
        if (_countText != null) _countText.text = countText;
        if (_typeImage != null && typeImage != null) _typeImage.sprite = typeImage;
        // Если image должен быть скрыт, если sprite == null
    }

    public void SetTypeText(string text)
    {
        if (_typeText != null) _typeText.text = text;
    }

    public void SetCountText(string text)
    {
        if (_countText != null) _countText.text = text;
    }

    public void SetTypeImage(Sprite sprite)
    {
        if (_typeImage != null)
        {
            _typeImage.sprite = sprite;
            _typeImage.gameObject.SetActive(sprite != null); // Показать/скрыть изображение
        }
    }
}
