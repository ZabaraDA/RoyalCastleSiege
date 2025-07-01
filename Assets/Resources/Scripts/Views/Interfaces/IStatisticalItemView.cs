using UnityEngine;

public interface IStatisticalItemView
{
    // ����� ��� ��������� ������ � �����������
    void UpdateDisplay(string typeText, string countText, Sprite typeImage = null);

    // �������������� ������, ���� ����� ������ ��������� ����������
    void SetTypeText(string text);
    void SetCountText(string text);
    void SetTypeImage(Sprite sprite);

}
