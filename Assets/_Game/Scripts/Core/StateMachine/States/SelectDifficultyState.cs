public class SelectDifficultyState : State
{
    private SelectDifficulty _selectDifficulty;

    public SelectDifficultyState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, SelectDifficulty selectDifficulty)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _selectDifficulty = selectDifficulty;
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