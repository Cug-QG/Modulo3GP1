using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI.Table;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject bombPrefab;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!GameManager.Instance.Playing()) { return; }
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        PlaceBomb();
    }

    private void Move(float inputX, float inputY)
    {
        rb.linearVelocity = new Vector2(inputX, inputY).normalized * speed;
        if (inputX == 0 && inputY == 0) { return; }
        float angle = Mathf.Atan2(-inputX, inputY) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(angle / 90) * 90 % 360);
    }

    private void PlaceBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bombPrefab, CalculateSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 output = transform.position;
        output.x = Mathf.Round(output.x) < transform.position.x ? Mathf.Round(output.x) + 0.5f : Mathf.Round(output.x) - 0.5f;
        output.y = Mathf.Round(output.y) < transform.position.y ? Mathf.Round(output.y) + 0.5f : Mathf.Round(output.y) - 0.5f;
        return output;
    }
}
