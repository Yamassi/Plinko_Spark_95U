using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _ballImage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private CircleCollider2D _circleCollider2D;
    public int Bid;
    private float _standTime;
    private float _contactTime = 0;
    private Tween _tween;

    private void OnValidate()
    {
        _ballImage = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void SetBallImage(Sprite sprite) => _ballImage.sprite = sprite;

    private void OnEnable()
    {
        RandomMove();

        AudioSystem.Instance.BallLaunch();
    }

    private void OnDisable()
    {
        _tween.Kill();
    }

    private void Update()
    {
        if (_rigidbody2D.velocity.y <= 0 && _rigidbody2D.velocity.x <= 0)
        {
            _standTime += Time.deltaTime;
            // Debug.Log($"Объект НЕ двигается {_standTime}");
        }
        else
        {
            _standTime = 0;
            // Debug.Log("Объект двигается");
        }

        if (_standTime > 3)
            RandomMove();
    }

    private void RandomMove()
    {
        Debug.Log("Random Move");
        float randomX = Random.Range(-0.06f, 0.06f);
        while (randomX == 0)
            randomX = Random.Range(-0.06f, 0.06f);

        _tween = transform.DOLocalMoveX(randomX, 0.1f);
    }

    private void PowerRandomMove()
    {
        Debug.Log("Random Move");
        float randomX = Random.Range(-0.15f, 0.15f);
        while (randomX == 0)
            randomX = Random.Range(-0.15f, 0.15f);

        _tween = transform.DOLocalMoveX(randomX, 0.1f);
    }

    public void BallPause() => _rigidbody2D.simulated = false;
    public void BallResume() => _rigidbody2D.simulated = true;
    public void DisableBallCollider() => _circleCollider2D.enabled = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log($"Trigger enter {other.gameObject.name} Coeficient {_coefficient}");
        _contactTime += Time.deltaTime;
        Debug.Log(_contactTime);
        if (other.TryGetComponent<Ball>(out Ball ball) && _contactTime >= 5)
        {
            PowerRandomMove();
            _contactTime = 0;
        }
    }
}