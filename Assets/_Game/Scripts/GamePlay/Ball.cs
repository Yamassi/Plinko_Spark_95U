using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _ballImage;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private float _standTime;
    private void OnValidate()
    {
        _ballImage = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }
    public void SetBallImage(Sprite sprite) => _ballImage.sprite = sprite;
    private void OnEnable()
    {
        RandomMove();

        AudioSystem.Instance.BallLaunch();
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

        if (_standTime > 5)
            RandomMove();

    }
    private void RandomMove()
    {
        Debug.Log("Random Move");
        float randomX = Random.Range(-0.06f, 0.06f);
        while (randomX == 0)
            randomX = Random.Range(-0.06f, 0.06f);

        transform.DOLocalMoveX(randomX, 0.1f);
    }
    public void BallPause() => _rigidbody2D.simulated = false;
    public void BallResume() => _rigidbody2D.simulated = true;
}