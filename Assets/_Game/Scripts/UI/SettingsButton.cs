using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _active, _inactive;
    public Action OnClicked;
    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable() => _button.onClick.AddListener(Clicked);

    private void OnDisable() => _button.onClick.RemoveListener(Clicked);

    private void Clicked() => OnClicked?.Invoke();

    public void SetActive()
    {
        _active.gameObject.SetActive(true);
        _inactive.gameObject.SetActive(false);
    }
    public void SetInactive()
    {
        _active.gameObject.SetActive(false);
        _inactive.gameObject.SetActive(true);
    }
}
