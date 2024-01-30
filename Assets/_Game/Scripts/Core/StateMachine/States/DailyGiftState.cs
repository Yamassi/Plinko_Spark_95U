using System;
using System.Globalization;
using System.Linq;
using Tretimi;

public class DailyGiftState : State
{
    private DailyGifts _dailyGifts;

    public DailyGiftState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, DailyGifts dailyGifts)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _dailyGifts = dailyGifts;
    }

    public override void ComponentsToggle(bool value)
    {
        _dailyGifts.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        _uIService.UpdateUI();
        UpdateDailyGifts();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _dailyGifts.Close.onClick.AddListener(GoToMainMenu);
        _bottom.Coins.AddCoins.onClick.AddListener(GoToInAppShop);

        for (int i = 0; i < _dailyGifts.Gifts.Count; i++)
        {
            _dailyGifts.Gifts[i].OnSelect += TakeGift;
        }
    }

    public override void Unsubsribe()
    {
        _dailyGifts.Close.onClick.RemoveListener(GoToMainMenu);
        _bottom.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);

        for (int i = 0; i < _dailyGifts.Gifts.Count; i++)
        {
            _dailyGifts.Gifts[i].OnSelect -= TakeGift;
        }
    }

    private void TakeGift(int id, int coins)
    {
        var dailyGiftData = _dataService.GetData().DailyGiftsData;

        dailyGiftData[id].IsTaked = true;
        dailyGiftData[id].TakedTime = DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo);
        _dataService.AddCoins(coins);
        UpdateDailyGifts();
    }

    private void UpdateDailyGifts()
    {
        var dailyGiftData = _dataService.GetData().DailyGiftsData;

        for (int i = 0; i < dailyGiftData.Count; i++)
        {
            bool isGiftTaked = dailyGiftData[i].IsTaked;

            if (isGiftTaked)
                _dailyGifts.Gifts[i].GiftTaked();
            else
                _dailyGifts.Gifts[i].OpenGift();
        }
    }
}