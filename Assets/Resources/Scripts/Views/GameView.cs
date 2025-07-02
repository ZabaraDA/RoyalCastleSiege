using UnityEngine;

public class GameView : MonoBehaviour, IGameView
{
    [SerializeField]
    private GameObject _spawnContainerGameObject;
    [SerializeField]
    private GameObject _shopButton;
    [SerializeField]
    private GameObject _shopPanel;
    [SerializeField]
    private GameObject _menuPanel;


    public void ChangePanel()
    {
        _shopPanel.SetActive(!_shopPanel.activeSelf);
        _menuPanel.SetActive(!_menuPanel.activeSelf);
    }
}
