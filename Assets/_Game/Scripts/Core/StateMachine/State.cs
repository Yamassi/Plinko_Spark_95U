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
    protected State(IStateSwitcher stateSwitcher, IUIService uIService, IDataService dataService)
    {
        _stateSwitcher = stateSwitcher;
        _uIService = uIService;
        _dataService = dataService;
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
}