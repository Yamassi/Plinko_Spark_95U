using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    public GamePlayState(IStateSwitcher stateSwitcher, IUIService uIService,
   IDataService dataService, GamePlayUI gamePlayUI,
   GamePlay gamePlay) : base(stateSwitcher, uIService,dataService)
    {
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }

    public override void ComponentsToggle(bool value)
    {
        throw new System.NotImplementedException();
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Subsribe()
    {
        throw new System.NotImplementedException();
    }

    public override void Unsubsribe()
    {
        throw new System.NotImplementedException();
    }
}
