using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] Transform BallPoint, MapPoint, SlotPoint;
    [SerializeField] private List<FallZone> _fallZone;
    public Action<float, int> OnBallFall;
    public Action OnBallFallPast;
    private GameObject _currentBall;
    private GamePlayMap _currentMap;
    public XSlots CurrentXSlots { get; private set; }

    private void OnEnable()
    {
        for (int i = 0; i < _fallZone.Count; i++)
        {
            _fallZone[i].OnBallFallPast += BallFallPast;
        }
    }

    private void OnDisable()
    {
        ClearGamePlay();
    }

    public void ClearGamePlay()
    {
        if (_currentMap != null)
            Destroy(_currentMap.gameObject);

        for (int i = 0; i < CurrentXSlots.AllXSlots.Count; i++)
        {
            Debug.Log($"Отписка от слота X {i}");
            CurrentXSlots.AllXSlots[i].OnBallFallToXSlot -= BallFallToXSlot;
        }

        if (CurrentXSlots != null)
            Destroy(CurrentXSlots.gameObject);


        Ball[] balls = BallPoint.GetComponentsInChildren<Ball>();

        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i].gameObject);
        }

        for (int i = 0; i < _fallZone.Count; i++)
        {
            _fallZone[i].OnBallFallPast -= BallFallPast;
        }
    }

    private void BallFallToXSlot(float coefficient, int bid) => OnBallFall?.Invoke(coefficient, bid);

    private void BallFallPast() => OnBallFallPast?.Invoke();

    public async Task SetBall(int ballIndex) =>
        _currentBall = await Tretimi.Assets.GetAsset<GameObject>($"Ball{ballIndex}");

    public void LaunchBall(int bid) => Instantiate(_currentBall, BallPoint).GetComponent<Ball>().Bid = bid;

    public async Task SetMap(int currentMap)
    {
        GameObject map = await Tretimi.Assets.GetAsset<GameObject>($"Map{currentMap}");
        _currentMap = Instantiate(map, MapPoint).GetComponent<GamePlayMap>();
    }

    public async Task SetSlots(int slots)
    {
        GameObject xSlots = await Tretimi.Assets.GetAsset<GameObject>($"Slots{slots}");
        CurrentXSlots = Instantiate(xSlots, SlotPoint).GetComponent<XSlots>();

        for (int i = 0; i < CurrentXSlots.AllXSlots.Count; i++)
        {
            Debug.Log($"Подписка на слот X {i}");
            CurrentXSlots.AllXSlots[i].OnBallFallToXSlot += BallFallToXSlot;
        }
    }
}