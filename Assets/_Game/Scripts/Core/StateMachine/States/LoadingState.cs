using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tretimi;
using UniRx;
using UnityEngine;

public class LoadingState : State
{
    private Loading _loading;

    public LoadingState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, Loading loading)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _loading = loading;
    }

    public override void ComponentsToggle(bool value)
    {
        _loading.gameObject.SetActive(value);
    }

    public override async void Enter()
    {
        _loading.Logo.transform.localScale = Vector3.zero;
        ComponentsToggle(true);
        CheckGifts();

        await UniTask.Delay(100);
        await _loading.Logo.transform.DOScale(Vector3.one, 1).SetEase(Ease.InOutElastic).ToUniTask();
        await UniTask.Delay(1000);

        var gifts = _dataService.GetData().DailyGiftsData;
        bool isGiftOpen = !gifts.Last().IsTaked;

        if (isGiftOpen)
            GoToDailyGift();
        else
            GoToMainMenu();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
    }

    public override void Subsribe()
    {
    }

    public override void Unsubsribe()
    {
    }

    private void CheckGifts()
    {
        int maxGifts = 6;
        var gifts = _dataService.GetData().DailyGiftsData;
        bool isLastGiftTaked = gifts.Last().IsTaked && gifts.Count < maxGifts;

        if (isLastGiftTaked)
        {
            DateTime lastGiftTime = Tretimi.Time.ConvertStringToDateTime(gifts.Last().TakedTime);
            bool isTimeToOpenNewGift = DateTime.Now >= lastGiftTime.AddMinutes(0.3f);

            if (isTimeToOpenNewGift)
            {
                gifts.Add(new DailyGiftData());
            }
        }
    }
}