using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerManager : MonoBehaviour
{
    Vector3 finalPos;
    private bool moveBlock = false;

    public float speed = 5;
    public float offset = 0.05f;

    bool blockMovement = false;

    public GameObject tail;
    private int blocks;

    public GameObject points;
    private LevelManager levelManager;
    private Transform tails;

    private void Start()
    {
        points = GameObject.Find("Points");
        tails = GameObject.Find("Tails").transform;
        levelManager = transform.parent.GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (moveBlock)
        {
            Vector3 dir = (finalPos - transform.position);
            float distance = Vector3.Distance(transform.position, finalPos);

            if (distance > offset)
            {
                transform.Translate(dir.normalized * speed * Time.deltaTime);
            }
            else
            {
                // When Block is reached then stop the block
                moveBlock = false;
                blockMovement = false;

                Vector3 pos = transform.position;
                transform.position = new Vector3(Convert.ToInt16(pos.x), Convert.ToInt16(pos.y), Convert.ToInt16(pos.z));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            Vector3 pos = other.transform.position;
            Destroy(other.gameObject);

            Instantiate(tail, pos, tail.transform.rotation, tails);
            levelManager.PointTaken();
        }
    }


    public void MoveBlock(Vector3 dir)
    {
        if (!blockMovement)
        {
            points.SetActive(false);

            blocks = GetMovableBlocks(dir);
            finalPos = transform.position;

            points.SetActive(true);

            if (blocks != 0)
            {
                if (dir == transform.right || dir == -transform.right)
                {
                    finalPos = new Vector3(finalPos.x + blocks, finalPos.y, finalPos.z);
                }
                if (dir == transform.up || dir == -transform.up)
                {
                    finalPos = new Vector3(finalPos.x, finalPos.y + blocks, finalPos.z);
                }

                // Spawing tail at start position
                Instantiate(tail, transform.position, tail.transform.rotation, tails);

                moveBlock = true;
                blockMovement = true;
            }
            else
            {
                Debug.Log("Oops! No space to Move");
            }
        }
        else
        {
            Debug.Log("Are You Trying to Outsmart me!!");
        }
    }

    public int GetMovableBlocks(Vector3 dir)
    {
        RaycastHit hit;
        int blocks = 0;

        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
        {
                Vector3 startPos = transform.position;
                Vector3 finalPos = hit.collider.transform.position;

                Vector3 temp = finalPos - startPos;

                if (temp.x != 0)
                {
                    blocks = Convert.ToInt16(temp.x);
                }
                if (temp.y != 0)
                {
                    blocks = Convert.ToInt16(temp.y);
                }
        }
        return (blocks < 0) ? (blocks + 1) : (blocks - 1);
    }
}
