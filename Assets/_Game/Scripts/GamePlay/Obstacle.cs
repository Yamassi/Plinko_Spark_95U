using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public SpriteRenderer ObstacleSprite;
    private void OnValidate()
    {
        ObstacleSprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSystem.Instance.BallFallToObstacle();
    }
}