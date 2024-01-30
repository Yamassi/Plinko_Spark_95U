using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        await UniTask.Delay(100);
        await _loading.Logo.transform.DOScale(Vector3.one, 1).SetEase(Ease.InOutElastic).ToUniTask();
        await UniTask.Delay(1000);
        _stateSwitcher.SwitchState<DailyGiftState>();
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
}