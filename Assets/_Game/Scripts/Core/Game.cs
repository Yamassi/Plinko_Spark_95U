using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Tretimi;
using System.Globalization;
using System.Threading.Tasks;

#if UNITY_IOS
using UnityEngine.iOS;
#endif
public class Game : IStateSwitcher, IUIService, IDataService
{
    private SaveData _data;
    private UIHolder _uIHolder;
    private StateMachine _stateMachine;
    public List<State> AllStates { get; private set; }
    private GamePlay _gamePlay;
    public Game(UIHolder uIHolder, GamePlay gamePlay)
    {
        _uIHolder = uIHolder;
        _gamePlay = gamePlay;
    }
    public async void Init()
    {
        LoadData();

        _stateMachine = new();

        AllStates = new(){
            new LoadingState(this,this,this,
            _uIHolder.Loading),
            new MainMenuState(this,this,this,
            _uIHolder.MainMenu),
            new SettingsState(this,this,this,
            _uIHolder.Settings),
            new ShopState(this,this,this,
            _uIHolder.Shop),
            new GamePlayState(this,this,this,
            _uIHolder.GamePlayUI, _gamePlay),
    };

        _stateMachine.Init(AllStates[0]);

        await UniTask.Delay(9000);
        RequestToRate();
    }

    private void LoadData()
    {
        SaveData saveData = DataProvider.LoadDataJSON();

        if (saveData is null)
        {
            List<int> resetedList = new() { 0 };

            Debug.Log("Saved Data is null and reseted");
            _data = new()
            {
                Money = Const.FirstMoney,
                Record = 0,
                AvailableEnergy = Const.StartEnergy,
                IsEnergyInfinity = false,
                AvailableBalls = new(resetedList),
                AvailableMapColors = new(resetedList),
                TimesToAddEnergy = new(),
            };

            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetFloat("SoundVolume", 1);

            PlayerPrefs.SetInt("CurrentMapType", 0);
            PlayerPrefs.SetInt("CurrentMapColor", 0);
            PlayerPrefs.SetInt("CurrentBall", 0);
            PlayerPrefs.SetInt("CurrentDifficultyMode", 0);
        }
        else
            _data = saveData;
    }
    public async Task ChangeBackground(int backgroundID)
    {
        _uIHolder.GameBackground.gameObject.SetActive(true);
        _uIHolder.GameBackground.sprite = await Assets.GetAsset<Sprite>($"Back{backgroundID}");
    }
    public void SwitchState<T>() where T : State
    {
        var state = AllStates.FirstOrDefault(s => s is T);
        _stateMachine.ChangeState(state);
    }

    public void UpdateUI()
    {
        
    }
    private void RequestToRate()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }

    public SaveData GetData()
    {
        return _data;
    }

    public void AddMoney(int money)
    {
        if (money > 0)
            _data.Money += money;

        if (_data.Money < 0)
            _data.Money = 0;
    }

    public void RemoveMoney(int money)
    {
        if (money > 0)
            _data.Money -= money;

        if (_data.Money < 0)
            _data.Money = 0;
    }
}
