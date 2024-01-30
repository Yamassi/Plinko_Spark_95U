using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyGift : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _reward;
    [SerializeField] private Button _open;
    [SerializeField] private TextMeshProUGUI _rewardText;
    public Action<int, int> OnSelect;

    private void OnValidate()
    {
        _open = GetComponentInChildren<Button>(true);
        _rewardText = GetComponentInChildren<TextMeshProUGUI>();
        _rewardText.text = _reward.ToString();
        _id = transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        _open.onClick.AddListener(Clicked);
    }

    private void OnDisable()
    {
        _open.onClick.RemoveListener(Clicked);
    }

    private void Clicked() => OnSelect?.Invoke(_id, _reward);

    public void OpenGift()
    {
        _open.interactable = true;
        _open.gameObject.SetActive(true);
    }

    public void CloseGift()
    {
        _open.gameObject.SetActive(false);
    }

    public void GiftTaked()
    {
        _open.interactable = false;
        _open.gameObject.SetActive(true);
    }
}