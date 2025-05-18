using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Bomb : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject explosionPrefab;

    void Start()
    {
        StartCoroutine(Timer(timer));
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        // Direzioni cardinali
        Vector2[] directions = new Vector2[] {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right};

        foreach (Vector2 dir in directions)
        {
            for (int i = 1; i <= 2; i++)
            {
                Vector2 origin = transform.position;
                Vector2 targetPos = origin + dir * i;

                // Raycast per controllare ostacoli
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, i, obstacleMask);
                if (hit.collider != null)
                {
                    // Fermati se colpisce qualcosa
                    if (hit.collider.CompareTag("Destructable")) 
                    {
                        Vector2 contactPoint = hit.point;
                        Vector2 normal = hit.normal;
                        Vector2 adjustedPoint = contactPoint - normal * 0.05f;

                        Vector3Int cellPos = hit.transform.GetComponent<Tilemap>().WorldToCell(adjustedPoint); //center of cell is +.5x +.5y
                        hit.transform.GetComponent<Tilemap>().SetTile(cellPos, null);
                    }
                    break;
                }

                // Altrimenti, crea l'esplosione
                Instantiate(explosionPrefab, targetPos, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}