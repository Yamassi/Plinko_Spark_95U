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
        _dailyTasks.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        CompleteTasks();
        UpdateTasks();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _bottom.Coins.AddCoins.onClick.AddListener(GoToInAppShop);
        _dailyTasks.Close.onClick.AddListener(GoToMainMenu);

        for (int i = 0; i < _dailyTasks.Tasks.Count; i++)
        {
            _dailyTasks.Tasks[i].OnGetReward += GetReward;
        }
    }

    public override void Unsubsribe()
    {
        _bottom.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);
        _dailyTasks.Close.onClick.RemoveListener(GoToMainMenu);

        for (int i = 0; i < _dailyTasks.Tasks.Count; i++)
        {
            _dailyTasks.Tasks[i].OnGetReward -= GetReward;
        }
    }

    private void CompleteTasks()
    {
        var data = _dataService.GetData();

        bool isSomethingBuyed = data.AvailableBalls.Count > 1 || data.AvailableMaps.Count > 1;
        bool isAllBallsBuyed = data.AvailableBalls.Count == 5;
        bool isAllMapsBuyed = data.AvailableMaps.Count == 4;

        if (isSomethingBuyed)
            data.DailyTasksData[2].IsComplete = true;

        if (isAllBallsBuyed)
            data.DailyTasksData[3].IsComplete = true;

        if (isAllMapsBuyed)
            data.DailyTasksData[4].IsComplete = true;
    }

    private void GetReward(int id, int reward)
    {
        _dataService.GetData().DailyTasksData[id].IsTaked = true;
        _dataService.AddCoins(reward);

        UpdateTasks();
    }

    private void UpdateTasks()
    {
        var dailyTasksData = _dataService.GetData().DailyTasksData;
        for (int i = 0; i < dailyTasksData.Count; i++)
        {
            if (dailyTasksData[i].IsComplete && dailyTasksData[i].IsTaked)
                _dailyTasks.Tasks[i].SetTaked();

            if (dailyTasksData[i].IsComplete && !dailyTasksData[i].IsTaked)
                _dailyTasks.Tasks[i].SetComplete();

            if (!dailyTasksData[i].IsComplete && !dailyTasksData[i].IsTaked)
                _dailyTasks.Tasks[i].SetUncomplete();
        }
    }
}