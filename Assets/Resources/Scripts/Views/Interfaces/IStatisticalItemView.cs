using UnityEngine;

public interface IStatisticalItemView
{
    // Метод для установки текста и изображения
    void UpdateDisplay(string typeText, string countText, Sprite typeImage = null);

    // Дополнительные методы, если нужны только частичные обновления
    void SetTypeText(string text);
    void SetCountText(string text);
    void SetTypeImage(Sprite sprite);

}
