using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SettingsState : State
{
    private Settings _settings;
    private bool _isSoundEnable;
    private bool _isVibrationEnable;

    public SettingsState(IStateSwitcher stateSwitcher, IUIService uIService, IDataService dataService,
        Top top, Bottom bottom, Settings settings) : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _settings = settings;
    }

    public override void ComponentsToggle(bool value)
    {
        _top.Logo.gameObject.SetActive(value);
        _settings.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        UpdateSettings();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _settings.Close.onClick.AddListener(GoToMainMenu);
        _settings.Sound.OnClicked += SwitchSound;
        _settings.Vibration.OnClicked += SwitchVibration;
    }

    public override void Unsubsribe()
    {
        _settings.Close.onClick.RemoveListener(GoToMainMenu);
        _settings.Sound.OnClicked -= SwitchSound;
        _settings.Vibration.OnClicked -= SwitchVibration;
    }

    private void SwitchVibration()
    {
        _isVibrationEnable = !_isVibrationEnable;
        PlayerPrefs.SetInt("Vibration", _isVibrationEnable ? 1 : 0);
        UpdateVibration();
    }

    private void SwitchSound()
    {
        _isSoundEnable = !_isSoundEnable;
        PlayerPrefs.SetInt("Sound", _isSoundEnable ? 1 : 0);
        AudioSystem.Instance.UpdateSoundVolume();
        UpdateSound();
    }


    private void UpdateSettings()
    {
        int sound = PlayerPrefs.GetInt("Sound");
        int vibration = PlayerPrefs.GetInt("Vibration");

        _isSoundEnable = sound == 1;
        _isVibrationEnable = vibration == 1;

        UpdateSound();
        UpdateVibration();
    }

    private void UpdateSound()
    {
        if (_isSoundEnable)
            _settings.Sound.SetActive();
        else
            _settings.Sound.SetInactive();
    }

    private void UpdateVibration()
    {
        if (_isVibrationEnable)
            _settings.Vibration.SetActive();
        else
            _settings.Vibration.SetInactive();
    }
}