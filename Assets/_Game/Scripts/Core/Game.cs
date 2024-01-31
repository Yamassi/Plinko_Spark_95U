using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Tretimi;
using System.Globalization;
using System.Threading.Tasks;
using UniRx;
using Time = UnityEngine.Time;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class Game : IStateSwitcher, IUIService, IDataService
{
    private SaveData _data;
    private UIHolder _uIHolder;
    private StateMachine _stateMachine;
    private List<State> _allStates;
    private GamePlay _gamePlay;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public Game(UIHolder uIHolder, GamePlay gamePlay)
    {
        _uIHolder = uIHolder;
        _gamePlay = gamePlay;
    }

    public async void Init()
    {
        LoadData();

        _stateMachine = new();

        _allStates = new()
        {
            new LoadingState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.Loading),
            new DailyGiftState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.DailyGifts),
            new MainMenuState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.MainMenu),
            new SettingsState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.Settings),
            new ShopState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.Shop),
            new DailyTaskState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.DailyTasks),
            new SelectDifficultyState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.SelectDifficulty),
            new GamePlayState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.GamePlayUI, _gamePlay),
            new InAppShopState(this, this, this,
                _uIHolder.Top, _uIHolder.Bottom, _uIHolder.InAppShop)
        };

        _stateMachine.Init(_allStates[0]);

        CheckTasks();

        await UniTask.Delay(12000);
        RequestToRate();
    }

    private void CheckTasks()
    {
        if (!_data.DailyTasksData[0].IsComplete)
        {
            DateTime lastLogInTime = Tretimi.Time.ConvertStringToDateTime(_data.LastLogInTime);
            if (DateTime.Now > lastLogInTime.AddMinutes(5))
            {
                _data.LogInCount++;

                if (_data.LogInCount == 10)
                    _data.DailyTasksData[0].IsComplete = true;
            }

            _data.LastLogInTime = DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo);
        }


        if (!_data.DailyTasksData[1].IsComplete)
        {
            float time = 0;
            int requiredMinutesInGame = 30;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                time += Time.deltaTime;
                float minutes = Mathf.Floor(time / 60);
                if (minutes >= requiredMinutesInGame)
                {
                    _data.DailyTasksData[1].IsComplete = true;
                    _disposable.Clear();
                }
            }).AddTo(_disposable);
        }
    }

    private void LoadData()
    {
        SaveData saveData = DataProvider.LoadDataJSON();

        if (saveData is null)
        {
            Debug.Log("Saved Data is null and reseted");
            _data = new()
            {
                Coins = Const.FirstCoins,
                AvailableBalls = new() { 0 },
                AvailableMaps = new() { 0 },
                DailyGiftsData = new() { new() },
                DailyTasksData = new()
                {
                    new(), new(), new(),
                    new(), new(),
                },
                LogInCount = 1,
                LastLogInTime = DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo),
            };

            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Vibration", 1);

            PlayerPrefs.SetInt("CurrentMap", 0);
            PlayerPrefs.SetInt("CurrentBall", 0);
            PlayerPrefs.SetInt("CurrentDifficulty", 0);
        }
        else
            _data = saveData;
    }

    public async Task ChangeBackground(int backgroundID)
    {
        _uIHolder.GameBackground.gameObject.SetActive(true);
        _uIHolder.GameBackground.sprite = await Assets.GetAsset<Sprite>($"Background{backgroundID}");
    }

    public void SwitchState<T>() where T : State
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _stateMachine.ChangeState(state);
    }

    public void UpdateUI()
    {
        string coins = _data.Coins.ToString();
        _uIHolder.Top.Coins.Coins.text = coins;
        _uIHolder.Bottom.Coins.Coins.text = coins;
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

    public void AddCoins(int money)
    {
        if (money > 0)
            _data.Coins += money;

        if (_data.Coins < 0)
            _data.Coins = 0;
        UpdateUI();
    }

    public void RemoveCoins(int money)
    {
        if (money > 0)
            _data.Coins -= money;

        if (_data.Coins < 0)
            _data.Coins = 0;
        UpdateUI();
    }
}