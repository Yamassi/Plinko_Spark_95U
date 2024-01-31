using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    [SerializeField] private Transform _start;
    public Action OnBallFallPast;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            OnBallFallPast?.Invoke();
            Destroy(ball.gameObject);
        }
    }
}