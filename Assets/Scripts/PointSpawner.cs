using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject point;
    RaycastHit hit;

    [SerializeField]
    private float distance = 1f;

    public Transform parent;

    void Start()
    {
        parent = GameObject.Find("Points").transform;

        // Raycast in all directions
        if (!Physics.Raycast(transform.position, Vector3.left, out hit, distance))
        {
            Vector3 pos = transform.position;
            Vector3 spawnPos = new Vector3(pos.x - 1, pos.y, pos.z);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

        if (!Physics.Raycast(transform.position, Vector3.right, out hit, distance))
        {
            Vector3 pos = transform.position;
            Vector3 spawnPos = new Vector3(pos.x + 1, pos.y, pos.z);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

        if (!Physics.Raycast(transform.position, Vector3.up, out hit, distance))
        {
            Vector3 pos = transform.position;
            Vector3 spawnPos = new Vector3(pos.x, pos.y + 1, pos.z);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

        if (!Physics.Raycast(transform.position, Vector3.down, out hit, distance))
        {
            Vector3 pos = transform.position;
            Vector3 spawnPos = new Vector3(pos.x, pos.y - 1, pos.z);

            Instantiate(point, spawnPos, point.transform.rotation, parent);
        }

    }
}
