using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    private int _currentBid;

    public GamePlayState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, GamePlayUI gamePlayUI,
        GamePlay gamePlay) : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }

    public override void ComponentsToggle(bool value)
    {
        _gamePlayUI.gameObject.SetActive(value);
        _gamePlay.gameObject.SetActive(value);
        _top.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        Init();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _gamePlayUI.IncreaseBid.onClick.AddListener(IncreaseBid);
        _gamePlayUI.DecreaseBid.onClick.AddListener(DecreaseBid);
        _gamePlayUI.Drop.onClick.AddListener(LaunchBall);
    }

    public override void Unsubsribe()
    {
        _gamePlayUI.IncreaseBid.onClick.RemoveListener(IncreaseBid);
        _gamePlayUI.DecreaseBid.onClick.RemoveListener(DecreaseBid);
        _gamePlayUI.Drop.onClick.RemoveListener(LaunchBall);
    }

    private void Init()
    {
        _currentBid = 100;
        _gamePlayUI.BidText.text = _currentBid.ToString();
    }

    private void LaunchBall()
    {
        
    }

    private void DecreaseBid()
    {
        if (_currentBid > 50)
            _currentBid -= 50;
        _gamePlayUI.BidText.text = _currentBid.ToString();
    }

    private void IncreaseBid()
    {
        _currentBid -= 50;
        _gamePlayUI.BidText.text = _currentBid.ToString();
    }
}