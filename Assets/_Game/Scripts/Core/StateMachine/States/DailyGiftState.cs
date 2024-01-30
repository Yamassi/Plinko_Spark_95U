public class DailyGiftState : State
{
    private DailyGifts _dailyGifts;

    public DailyGiftState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, DailyGifts dailyGifts)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _dailyGifts = dailyGifts;
    }

    public override void ComponentsToggle(bool value)
    {
        _dailyGifts.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        _uIService.UpdateUI();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _dailyGifts.Close.onClick.AddListener(GoToMainMenu);
        _bottom.Coins.AddCoins.onClick.AddListener(GoToInAppShop);
    }

    public override void Unsubsribe()
    {
        _dailyGifts.Close.onClick.RemoveListener(GoToMainMenu);
        _bottom.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);
    }
}