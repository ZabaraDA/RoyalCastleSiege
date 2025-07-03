using System;
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
    private GameObject _costContainer;

    IStatisticalItemModel _coinsStatisticalItemModel;

    public bool IsBuy {  get; private set; }

    private IProjectileTypeModel _projectileTypeModel;
    public IProjectileTypeModel ProjectileTypeModel
    {
        get 
        { 
            return _projectileTypeModel; 
        }
        set
        {
            if (_projectileTypeModel != value)
            {
                _projectileTypeModel = value;
                OnViewProjectileTypeModelChanged?.Invoke(_projectileTypeModel);
            }
        }
    }

    public event Action<IProjectileTypeModel> OnClickBuyPrjectileTriggered;
    public event Action<IProjectileTypeModel> OnViewProjectileTypeModelChanged;

    private void Start()
    {
        _buyButton.onClick.AddListener(OnClick);
        OnViewProjectileTypeModelChanged += HandleOnViewProjectileTypeModelChanged;
    }

    private void HandleOnViewProjectileTypeModelChanged(IProjectileTypeModel projectileModel)
    {
        _costText.text = projectileModel.Cost.ToString();
        _itemImage.sprite = projectileModel.Sprite;
        OnClickBuyPrjectileTriggered?.Invoke(projectileModel);
    }
    private void OnClick()
    {
        if (_coinsStatisticalItemModel.Count >= ProjectileTypeModel.Cost)
        {
            OnClickBuyPrjectileTriggered?.Invoke(ProjectileTypeModel);
            _costContainer.gameObject.SetActive(false);
            _buyButton.interactable = false;
        }

    }
    public void SetProjectile(IProjectileTypeModel projectileModel)
    {
        _costText.text = projectileModel.Cost.ToString();
        _itemImage.sprite = projectileModel.Sprite;
        ProjectileTypeModel = projectileModel;
    }
    public void SetCoinsStatisticalItemModel(IStatisticalItemModel coinsStatisticalItemModel) 
    {
        _coinsStatisticalItemModel = coinsStatisticalItemModel;
    }
}