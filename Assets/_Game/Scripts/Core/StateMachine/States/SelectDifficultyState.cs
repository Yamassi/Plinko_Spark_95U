using UnityEngine;

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
        _selectDifficulty.gameObject.SetActive(value);
        _top.Logo.gameObject.SetActive(value);
        _bottom.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        _uIService.ChangeBackground(0);
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _bottom.Coins.AddCoins.onClick.AddListener(GoToInAppShop);
        _selectDifficulty.Back.onClick.AddListener(GoToMainMenu);

        _selectDifficulty.Easy.onClick.AddListener(StartEasyMode);
        _selectDifficulty.Medium.onClick.AddListener(StartMediumMode);
        _selectDifficulty.Hard.onClick.AddListener(StartHardMode);
    }

    public override void Unsubsribe()
    {
        _bottom.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);
        _selectDifficulty.Back.onClick.RemoveListener(GoToMainMenu);

        _selectDifficulty.Easy.onClick.RemoveListener(StartEasyMode);
        _selectDifficulty.Medium.onClick.RemoveListener(StartMediumMode);
        _selectDifficulty.Hard.onClick.RemoveListener(StartHardMode);
    }

    private void StartEasyMode()
    {
        PlayerPrefs.SetInt("CurrentDifficulty", 0);
        GoToGamePlay();
    }

    private void StartMediumMode()
    {
        PlayerPrefs.SetInt("CurrentDifficulty", 1);
        GoToGamePlay();
    }

    private void StartHardMode()
    {
        PlayerPrefs.SetInt("CurrentDifficulty", 2);
        GoToGamePlay();
    }
}