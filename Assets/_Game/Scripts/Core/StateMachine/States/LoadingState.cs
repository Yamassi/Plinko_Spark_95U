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
    private CompositeDisposable _disposable = new();
    public LoadingState(IStateSwitcher stateSwitcher, IUIService uIService, IDataService dataService, Loading loading)
    : base(stateSwitcher, uIService,dataService)
    {
        _loading = loading;
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
