using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject rightEdge;
    public GameObject leftEdge;
    public GameObject upEdge;
    public GameObject downEdge;

    RaycastHit hit;

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
        if (Physics.Raycast(transform.position, Vector3.left, out hit, distance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                leftEdge.SetActive(false);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, distance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                rightEdge.SetActive(false);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.up, out hit, distance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                upEdge.SetActive(false);
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                downEdge.SetActive(false);
            }
        }

    }
}
