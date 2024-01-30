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
        IDataService dataService, Top top, Bottom bottom, Shop shop) : base(stateSwitcher, uIService, dataService, top,
        bottom)
    {
        _shop = shop;
    }

    public override void ComponentsToggle(bool value)
    {
        _shop.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        OpenMaps();
        UpdateShop();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _bottom.Coins.AddCoins.onClick.AddListener(GoToInAppShop);
        _shop.Next.onClick.AddListener(OpenBalls);
        _shop.Back.onClick.AddListener(OpenMaps);
        _shop.Close.onClick.AddListener(GoToMainMenu);

        for (int i = 0; i < _shop.ShopMaps.Maps.Count; i++)
        {
            _shop.ShopMaps.Maps[i].OnTryToBuy += TryToBuyMap;
            _shop.ShopMaps.Maps[i].OnSelect += SelectMap;
        }

        for (int i = 0; i < _shop.ShopBalls.Balls.Count; i++)
        {
            _shop.ShopBalls.Balls[i].OnTryToBuy += TryToBuyBall;
            _shop.ShopBalls.Balls[i].OnSelect += SelectBall;
        }
    }

    public override void Unsubsribe()
    {
        _bottom.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);
        _shop.Next.onClick.RemoveListener(OpenBalls);
        _shop.Back.onClick.RemoveListener(OpenMaps);
        _shop.Close.onClick.RemoveListener(GoToMainMenu);

        for (int i = 0; i < _shop.ShopMaps.Maps.Count; i++)
        {
            _shop.ShopMaps.Maps[i].OnTryToBuy -= TryToBuyMap;
            _shop.ShopMaps.Maps[i].OnSelect -= SelectMap;
        }

        for (int i = 0; i < _shop.ShopBalls.Balls.Count; i++)
        {
            _shop.ShopBalls.Balls[i].OnTryToBuy -= TryToBuyBall;
            _shop.ShopBalls.Balls[i].OnSelect -= SelectBall;
        }
    }

    private void SelectBall(int id)
    {
        PlayerPrefs.SetInt("CurrentBall", id);
        UpdateShop();
    }

    private void TryToBuyBall(int id, int price)
    {
        if (_dataService.GetData().Coins >= price)
        {
            _dataService.RemoveCoins(price);
            _dataService.GetData().AvailableBalls.Add(id);
            UpdateShop();
        }
    }

    private void SelectMap(int id)
    {
        PlayerPrefs.SetInt("CurrentMap", id);
        UpdateShop();
    }

    private void TryToBuyMap(int id, int price)
    {
        if (_dataService.GetData().Coins >= price)
        {
            _dataService.RemoveCoins(price);
            _dataService.GetData().AvailableMaps.Add(id);
        }

        UpdateShop();
    }

    private void OpenMaps()
    {
        _shop.ShopMaps.gameObject.SetActive(true);
        _shop.ShopBalls.gameObject.SetActive(false);
        _shop.Back.gameObject.SetActive(false);
        _shop.Next.gameObject.SetActive(true);
    }

    private void OpenBalls()
    {
        _shop.ShopMaps.gameObject.SetActive(false);
        _shop.ShopBalls.gameObject.SetActive(true);
        _shop.Back.gameObject.SetActive(true);
        _shop.Next.gameObject.SetActive(false);
    }

    private void UpdateShop()
    {
        var availableMaps = _dataService.GetData().AvailableMaps;
        var availableBalls = _dataService.GetData().AvailableBalls;

        for (int i = 0; i < _shop.ShopMaps.Maps.Count; i++)
        {
            if (availableMaps.Contains(i))
                _shop.ShopMaps.Maps[i].SetBuyed();
            else
                _shop.ShopMaps.Maps[i].SetPrice();
        }

        for (int i = 0; i < _shop.ShopBalls.Balls.Count; i++)
        {
            if (availableBalls.Contains(i))
                _shop.ShopBalls.Balls[i].SetBuyed();
            else
                _shop.ShopBalls.Balls[i].SetPrice();
        }

        int currentMap = PlayerPrefs.GetInt("CurrentMap");
        int currentBall = PlayerPrefs.GetInt("CurrentBall");

        _shop.ShopMaps.Maps[currentMap].SetSelected();
        _shop.ShopBalls.Balls[currentBall].SetSelected();
    }
}