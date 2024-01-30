using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class State
{
    protected readonly IStateSwitcher _stateSwitcher;
    protected readonly IUIService _uIService;
    protected readonly IDataService _dataService;
    protected readonly Top _top;
    protected readonly Bottom _bottom;

    protected State(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom)
    {
        _stateSwitcher = stateSwitcher;
        _uIService = uIService;
        _dataService = dataService;
        _top = top;
        _bottom = bottom;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Subsribe();
    public abstract void Unsubsribe();
    public abstract void ComponentsToggle(bool value);

    protected async Task SetBackground(int backgroundID)
    {
        await _uIService.ChangeBackground(backgroundID);
    }

    public void GoToMainMenu() => _stateSwitcher.SwitchState<MainMenuState>();
    public void GoToInAppShop() => _stateSwitcher.SwitchState<InAppShopState>();
    public void GoToDailyGift() => _stateSwitcher.SwitchState<DailyGiftState>();
}