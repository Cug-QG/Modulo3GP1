using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float lifetime;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { GameManager.Instance.GameOver(); }
        if (collision.CompareTag("Enemy")) { collision.GetComponent<Enemy>().Die(); }
    }
}
