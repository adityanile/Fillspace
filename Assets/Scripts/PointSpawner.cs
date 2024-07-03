using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject point;
    RaycastHit2D hit;

    [SerializeField]
    private float distance = 1f;

    public Transform parent;

    void Start()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.left, distance);
        if (hit.collider == null)
        {
            Vector2 pos = transform.position;
            Vector2 spawnPos = new Vector2(pos.x - 1, pos.y);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right, distance);
        if (hit.collider == null)
        {
            Vector2 pos = transform.position;
            Vector2 spawnPos = new Vector2(pos.x + 1, pos.y);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.up, distance);
        if (hit.collider == null)
        {
            Vector2 pos = transform.position;
            Vector2 spawnPos = new Vector2(pos.x, pos.y + 1);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.down, distance);
        if (hit.collider == null)
        {
            Vector2 pos = transform.position;
            Vector2 spawnPos = new Vector2(pos.x, pos.y - 1);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

    }
}
