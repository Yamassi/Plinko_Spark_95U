using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class GamePlayState : State
{
    private GamePlayUI _gamePlayUI;
    private GamePlay _gamePlay;
    private int _currentBid;
    private bool _isLaunchAvailable = true;
    private CancellationTokenSource _cts, _cts2;
    private Tween _tween;
    private bool _isVibrationOn;

    public GamePlayState(IStateSwitcher stateSwitcher, IUIService uIService,
        IDataService dataService, Top top, Bottom bottom, GamePlayUI gamePlayUI,
        GamePlay gamePlay) : base(stateSwitcher, uIService, dataService, top, bottom)
    {
        _gamePlayUI = gamePlayUI;
        _gamePlay = gamePlay;
    }

    public override void ComponentsToggle(bool value)
    {
        _gamePlayUI.gameObject.SetActive(value);
        _gamePlay.gameObject.SetActive(value);
        _top.Coins.gameObject.SetActive(value);
    }

    public override void Enter()
    {
        ComponentsToggle(true);
        Subsribe();
        Init();
    }

    public override void Exit()
    {
        ComponentsToggle(false);
        Unsubsribe();
    }

    public override void Subsribe()
    {
        _top.Coins.AddCoins.onClick.AddListener(GoToInAppShop);
        _gamePlayUI.IncreaseBid.onClick.AddListener(IncreaseBid);
        _gamePlayUI.DecreaseBid.onClick.AddListener(DecreaseBid);
        _gamePlayUI.Drop.onClick.AddListener(LaunchBall);
        _gamePlayUI.Close.onClick.AddListener(GoToSelectDifficulty);
        _gamePlay.OnBallFall += BallFallToSlot;
        _gamePlay.OnBallFallPast += LaunchBall;

        _cts = new();
        _cts2 = new();
    }

    public override void Unsubsribe()
    {
        _top.Coins.AddCoins.onClick.RemoveListener(GoToInAppShop);
        _gamePlayUI.IncreaseBid.onClick.RemoveListener(IncreaseBid);
        _gamePlayUI.DecreaseBid.onClick.RemoveListener(DecreaseBid);
        _gamePlayUI.Drop.onClick.RemoveListener(LaunchBall);
        _gamePlayUI.Close.onClick.RemoveListener(GoToSelectDifficulty);
        _gamePlay.OnBallFall -= BallFallToSlot;
        _gamePlay.OnBallFallPast -= LaunchBall;

        _cts.Cancel();
        _cts.Dispose();
        _cts2.Cancel();
        _cts2.Dispose();
        _tween.Kill();
    }

    private async void Init()
    {
        _currentBid = 100;
        _gamePlayUI.BidText.text = _currentBid.ToString();

        await _uIService.ChangeBackground(PlayerPrefs.GetInt("CurrentMap"));

        await _gamePlay.SetBall(PlayerPrefs.GetInt("CurrentBall"));
        await _gamePlay.SetMap(PlayerPrefs.GetInt("CurrentMap"));
        await _gamePlay.SetSlots(PlayerPrefs.GetInt("CurrentDifficulty"));

        _isLaunchAvailable = true;
        _isVibrationOn = PlayerPrefs.GetInt("Vibration") == 1;

        _gamePlayUI.WinScore.gameObject.SetActive(false);
        _gamePlayUI.LoseScore.gameObject.SetActive(false);
    }

    private async void BallFallToSlot(float coefficient, int bid)
    {
        Debug.Log("Ball Fall To Slot");
        Vector3 scalePunch = new Vector3(0.3f, 0.3f, 0.3f);
        float floatPrize = (bid * coefficient);
        int prize = (int)floatPrize;

        if (coefficient < 1)
        {
            _dataService.AddCoins(prize);
            Debug.Log("Lose Score " + prize);
            AudioSystem.Instance.LoseSound();

            _gamePlayUI.LoseScore.transform.localScale = Vector3.one;
            _gamePlayUI.LoseScore.gameObject.SetActive(true);
            _gamePlayUI.LoseScoreText.text = $"-{prize}";
            _tween = _gamePlayUI.LoseScore.transform.DOPunchScale(scalePunch, 0.3f);
            await UniTask.Delay(500, cancellationToken: _cts2.Token);
            _gamePlayUI.LoseScore.gameObject.SetActive(false);
        }

        if (coefficient >= 1)
        {
            _dataService.AddCoins(prize);
            Debug.Log("Win Score " + prize);
            AudioSystem.Instance.WinSound();

            _gamePlayUI.WinScore.transform.localScale = Vector3.one;
            _gamePlayUI.WinScore.gameObject.SetActive(true);
            _gamePlayUI.WinScoreText.text = $"+{prize}";
            ;
            _tween = _gamePlayUI.WinScore.transform.DOPunchScale(scalePunch, 0.3f);
            await UniTask.Delay(500, cancellationToken: _cts2.Token);
            _gamePlayUI.WinScore.gameObject.SetActive(false);
        }

        if (_isVibrationOn)
            Handheld.Vibrate();
    }

    private async void LaunchBall()
    {
        if (_isLaunchAvailable && _dataService.GetData().Coins >= _currentBid)
        {
            _gamePlay.LaunchBall(_currentBid);
            _isLaunchAvailable = false;
            _dataService.RemoveCoins(_currentBid);

            await UniTask.Delay(1200, cancellationToken: _cts.Token);
            _isLaunchAvailable = true;
        }
    }

    private void DecreaseBid()
    {
        if (_currentBid > 50)
            _currentBid -= 50;
        _gamePlayUI.BidText.text = _currentBid.ToString();
    }

    private void IncreaseBid()
    {
        _currentBid += 50;
        _gamePlayUI.BidText.text = _currentBid.ToString();
    }
}