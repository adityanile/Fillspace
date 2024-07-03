using System;
using UnityEngine;

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
        GameObject tail = new GameObject("Tails");
        tail.transform.SetParent(gameObject.transform.parent);
        tails = tail.transform;

        levelManager = transform.parent.GetComponent<LevelManager>();

        points = levelManager.points;
    }

    private void Update()
    {
        if (moveBlock)
        {
            Vector2 dir = (finalPos - transform.position);
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

                Vector2 pos = transform.position;
                transform.position = new Vector2(Convert.ToInt16(pos.x), Convert.ToInt16(pos.y));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            Vector2 pos = collision.transform.position;
            Destroy(collision.gameObject);

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
                    finalPos = new Vector2(finalPos.x + blocks, finalPos.y);
                }
                if (dir == transform.up || dir == -transform.up)
                {
                    finalPos = new Vector2(finalPos.x, finalPos.y + blocks);
                }

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
        RaycastHit2D hit;
        int blocks = 0;

        hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity);
        if(hit.collider != null)
        {
            Vector2 startPos = transform.position;
            Vector2 finalPos = hit.collider.transform.position;

            Vector2 temp = finalPos - startPos;

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
