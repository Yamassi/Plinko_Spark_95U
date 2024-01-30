using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SettingsState : State
{
    private Settings _settings;

    public SettingsState(IStateSwitcher stateSwitcher, IUIService uIService, IDataService dataService,
        Top top, Bottom bottom, Settings settings) : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _settings = settings;
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