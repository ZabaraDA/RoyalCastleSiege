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

    private void Start()
    {
        _shopPanel.SetActive(false);
    }
    public void ChangePanel()
    {
        _shopPanel.SetActive(!_shopPanel.activeSelf);
        //_menuPanel.SetActive(!_menuPanel.activeSelf);
    }
}
