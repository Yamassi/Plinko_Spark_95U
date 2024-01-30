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

    public MainMenuState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom,
        MainMenu mainMenu)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _mainMenu = mainMenu;
    }

    public override void ComponentsToggle(bool value)
    {
        _mainMenu.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _bottom.Coins.AddCoins.onClick.AddListener(GoToInAppShop);
    }

    public override void Unsubsribe()
    {
        _bottom.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);
    }
}