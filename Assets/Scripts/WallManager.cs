using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject rightEdge;
    public GameObject leftEdge;
    public GameObject upEdge;
    public GameObject downEdge;
    
    RaycastHit2D hit;

    [SerializeField]
    private float distance = 1f;

    // Check at which side we have to add the edge dynamically
    
    void Start()
    {
        rightEdge.SetActive(true);
        leftEdge.SetActive(true);
        upEdge.SetActive(true);
        downEdge.SetActive(true);

        // Raycast in all directions
        
        hit = Physics2D.Raycast(transform.position, Vector2.right, distance);
        if (hit.collider != null)
        {
            rightEdge.SetActive(false);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.left, distance);
        if (hit.collider != null)
        {
            leftEdge.SetActive(false);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.up, distance);
        if (hit.collider != null)
        {
            upEdge.SetActive(false);
        }

        hit = Physics2D.Raycast(transform.position, Vector2.down, distance);
        if (hit.collider != null)
        {
            downEdge.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            Destroy(collision.gameObject);
        }
    }

}
