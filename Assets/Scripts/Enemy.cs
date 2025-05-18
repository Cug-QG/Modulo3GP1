using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask obstacleLayer;
    private Vector3 targetPosition;
    Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

    private void Start()
    {
        ChooseNewDirection();
    }

    private void Update()
    {
        if (transform.position == targetPosition)
        {
            ChooseNewDirection();
            return;
        }
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void ChooseNewDirection()
    {
        Vector3 chosenDirection = directions[Random.Range(0, directions.Length)];
        RaycastHit2D hit = Physics2D.Raycast(transform.position, chosenDirection, Mathf.Infinity, obstacleLayer);
        if (hit.collider != null)
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            int moveDistance = (int) Mathf.Round(distance - 0.5f);
            moveDistance = Random.Range(0, moveDistance + 1);
            targetPosition = transform.position + chosenDirection * moveDistance;
        }
        else { ChooseNewDirection(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { GameManager.Instance.GameOver(); }
    }

    public void Die()
    {
        GameManager.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}