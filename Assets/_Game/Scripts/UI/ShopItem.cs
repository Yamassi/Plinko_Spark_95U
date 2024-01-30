using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _buyBlock, _selectBlock;
    [SerializeField] private Button _buy, _select;
    [SerializeField] private TextMeshProUGUI _priceText, _selectText;
    [SerializeField] private Image _checkMark;
    [SerializeField] private Color _selectedColor;
    public Action<int, int> OnTryToBuy;
    public Action<int> OnSelect;

    private void Awake() => _selectedColor.a = 1;

    private void OnValidate()
    {
        _priceText.text = _price.ToString();
        _id = transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        _buy.onClick.AddListener(TryToBuy);
        _select.onClick.AddListener(Selected);
    }

    private void OnDisable()
    {
        _buy.onClick.RemoveListener(TryToBuy);
        _select.onClick.RemoveListener(Selected);
    }

    private void TryToBuy() => OnTryToBuy?.Invoke(_id, _price);
    private void Selected() => OnSelect?.Invoke(_id);

    public void SetPrice()
    {
        _buyBlock.gameObject.SetActive(true);
        _buy.gameObject.SetActive(true);
        _selectBlock.gameObject.SetActive(false);
        _select.gameObject.SetActive(false);
    }

    public void SetBuyed()
    {
        _buyBlock.gameObject.SetActive(false);
        _buy.gameObject.SetActive(false);
        _selectBlock.gameObject.SetActive(true);
        _select.gameObject.SetActive(true);
        _selectText.text = "Choose";
        _selectText.color = Color.white;
        _checkMark.gameObject.SetActive(false);
    }

    public void SetSelected()
    {
        _buyBlock.gameObject.SetActive(false);
        _buy.gameObject.SetActive(false);
        _selectBlock.gameObject.SetActive(true);
        _select.gameObject.SetActive(true);
        _selectText.text = "Selected";
        _selectText.color = _selectedColor;
        _checkMark.gameObject.SetActive(true);
    }
}