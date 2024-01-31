using System.Collections;
using UnityEngine;
using Tretimi;
using TMPro;
using System;
using DG.Tweening;

public class XSlot : MonoBehaviour
{
    [SerializeField] private float _coefficient;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Transform _fx;
    [SerializeField] private SpriteRenderer _background;
    public Action<float, int> OnBallFallToXSlot;
    private static Vector3 _defaultSize = new Vector3(1.2f, 1.2f, 1.2f);
    private Tween _tween, _tween2;

    private void Awake()
    {
        if (_coefficient <= 0.5f)
        {
            Color color = ColorSet.Instance.Colors[4];
            color.a = 1;
            _background.color = color;
        }

        if (_coefficient <= 1 && _coefficient > 0.5f)
        {
            Color color = ColorSet.Instance.Colors[3];
            color.a = 1;
            _background.color = color;
        }
        if (_coefficient < 1.5 && _coefficient > 1f)
        {
            Color color = ColorSet.Instance.Colors[2];
            color.a = 1;
            _background.color = color;
        }
        if (_coefficient < 2 && _coefficient >= 1.5f)
        {
            Color color = ColorSet.Instance.Colors[1];
            color.a = 1;
            _background.color = color;
        }
        if (_coefficient >= 2f)
        {
            Color color = ColorSet.Instance.Colors[0];
            color.a = 1;
            _background.color = color;
        }
            
    }

    private void OnEnable()
    {
        transform.localScale = _defaultSize;
    }

    private void OnDisable()
    {
        _tween.Kill();
        _tween2.Kill();
    }

    private void OnValidate()
    {
        _text = GetComponentInChildren<TextMeshPro>();
        _text.text = _coefficient.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Trigger enter {other.gameObject.name} Coeficient {_coefficient}");
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            ball.DisableBallCollider();
            int bid = ball.Bid;

            OnBallFallToXSlot?.Invoke(_coefficient, bid);
            AudioSystem.Instance.BallFallToSlot();
            Destroy(other.gameObject);

            _tween = transform.DOShakeScale(0.3f).OnComplete(() => transform.localScale = _defaultSize);

            _fx.transform.localScale = Vector3.one;
            _fx.gameObject.SetActive(true);
            _tween2 = _fx.transform.DOShakeScale(0.2f).OnComplete(() =>
            {
                _fx.gameObject.SetActive(false);
                _fx.transform.localScale = Vector3.one;
            });
        }
    }
}