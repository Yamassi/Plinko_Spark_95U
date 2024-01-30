public class InAppShopState : State
{
    private InAppShop _inAppShop;

    public InAppShopState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, InAppShop inAppShop)
        : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _inAppShop = inAppShop;
    }

    public override void ComponentsToggle(bool value)
    {
        _inAppShop.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _inAppShop.Close.onClick.AddListener(GoToMainMenu);
    }

    public override void Unsubsribe()
    {
        _inAppShop.Close.onClick.RemoveListener(GoToMainMenu);
    }
}