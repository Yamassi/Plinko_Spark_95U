using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Transform BallPoint, MapPoint, SlotPoint;
    public Action<float> OnBallFall;
    private GameObject _currentBall;
    private GamePlayMap _currentMap;
    public XSlots CurrentXSlots { get; private set; }
    private void OnDisable()
    {
        ClearGamePlay();
    }
    public void ClearGamePlay()
    {

        Destroy(_currentMap.gameObject);

        for (int i = 0; i < CurrentXSlots.AllXSlots.Count; i++)
        {
            Debug.Log($"Отписка от слота X {i}");
            CurrentXSlots.AllXSlots[i].OnBallFallToXSlot -= BallFallToXSlot;
        }
        Destroy(CurrentXSlots.gameObject);


        Ball[] balls = BallPoint.GetComponentsInChildren<Ball>();

        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i].gameObject);
        }
    }

    private void BallFallToXSlot(float coefficient)
    {
        OnBallFall?.Invoke(coefficient);
    }
    public async Task SetBall(int ballIndex)
    {
        _currentBall = await Tretimi.Assets.GetAsset<GameObject>($"Ball{ballIndex}");
    }

    public void LaunchBall()
    {
        Instantiate(_currentBall, BallPoint);
    }

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