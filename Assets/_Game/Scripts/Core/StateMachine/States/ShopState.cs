using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ShopState : State
{
    private Shop _shop;
    private int _currentShopItem;
    public ShopState(IStateSwitcher stateSwitcher, IUIService uIService,
   IDataService dataService, Shop shop) : base(stateSwitcher, uIService, dataService)
    {
        _shop = shop;
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
