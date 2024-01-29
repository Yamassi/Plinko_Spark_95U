using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tretimi;
using UniRx;
using UnityEngine;

public class MainMenuState : State
{
    private MainMenu _mainMenu;
    private int _currentMapColor, _currentMapType;
    private CompositeDisposable _disposable = new();
    private bool _isGameAvailable;
    public MainMenuState(IStateSwitcher stateSwitcher, IUIService uIService, IDataService dataService,
    MainMenu mainMenu)
    : base(stateSwitcher, uIService,dataService)
    {
        _mainMenu = mainMenu;
    }

    public override void ComponentsToggle(bool value)
    {
        throw new NotImplementedException();
    }

    public override void Enter()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }

    public override void Subsribe()
    {
        throw new NotImplementedException();
    }

    public override void Unsubsribe()
    {
        throw new NotImplementedException();
    }
}
