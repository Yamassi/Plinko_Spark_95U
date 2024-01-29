using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyTask : MonoBehaviour
{
    [SerializeField] private int _id, _reward;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _uncomplete, _complete;
    [SerializeField] private TextMeshProUGUI _price;
    public Action<int, int> OnGetReward; 
    private void OnValidate()
    {
        _button = GetComponent<Button>();
        _id = transform.GetSiblingIndex();
        _price.text = _reward.ToString();
    }

    private void OnEnable() => _button.onClick.AddListener(GetReward);

    private void OnDisable() => _button.onClick.RemoveListener(GetReward);

    private void GetReward() => OnGetReward?.Invoke(_id, _reward);

    public void SetUncomplete()
    {
        _button.interactable = false;
        _uncomplete.gameObject.SetActive(true);
        _complete.gameObject.SetActive(false);
    }

    public void Complete()
    {
        _button.interactable = true;
        _uncomplete.gameObject.SetActive(true);
        _complete.gameObject.SetActive(false);
    }

    public void Taked()
    {
        _button.interactable = false;
        _uncomplete.gameObject.SetActive(false);
        _complete.gameObject.SetActive(true);
    }
}
