using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;

    public GamePlayState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, GamePlayUI gamePlayUI,
        GamePlay gamePlay) : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }

    public override void ComponentsToggle(bool value)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Subsribe()
    {
    }

    public override void Unsubsribe()
    {
    }
}