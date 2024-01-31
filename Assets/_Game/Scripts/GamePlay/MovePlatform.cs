using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float _moveWidth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isReverse;
    private bool _isAnimate;
    private CancellationTokenSource _cts, _cts2;

    private async void OnEnable()
    {
        _cts = new();
        _cts2 = new();
        _isAnimate = true;
        var position = transform.position;

        if (!_isReverse)
            while (_isAnimate)
            {
                await transform.DOLocalMoveX(position.x + _moveWidth, _moveSpeed).SetEase(Ease.InOutSine)
                    .ToUniTask(cancellationToken: _cts.Token);
                await transform.DOLocalMoveX(position.x + -_moveWidth, _moveSpeed).SetEase(Ease.InOutSine)
                    .ToUniTask(cancellationToken: _cts2.Token);
            }
        else
            while (_isAnimate)
            {
                await transform.DOLocalMoveX(position.x + -_moveWidth, _moveSpeed).SetEase(Ease.InOutSine)
                    .ToUniTask(cancellationToken: _cts.Token);
                await transform.DOLocalMoveX(position.x + _moveWidth, _moveSpeed).SetEase(Ease.InOutSine)
                    .ToUniTask(cancellationToken: _cts2.Token);
            }
    }

    private void OnDisable()
    {
        _isAnimate = false;

        _cts.Cancel();
        _cts.Dispose();
        _cts2.Cancel();
        _cts2.Dispose();
    }
}