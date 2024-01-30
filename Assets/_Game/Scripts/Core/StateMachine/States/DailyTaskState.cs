public class DailyTaskState : State
{
    private DailyTasks _dailyTasks;

    public DailyTaskState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, DailyTasks dailyTasks)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _dailyTasks = dailyTasks;
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