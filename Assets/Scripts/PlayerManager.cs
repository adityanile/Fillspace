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

    public GameManager gameManager;

    private void Start()
    {
        GameObject tail = new GameObject("Tails");
        tail.transform.SetParent(gameObject.transform.parent);
        tails = tail.transform;

        levelManager = transform.parent.GetComponent<LevelManager>();

        points = levelManager.points;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

                // Check for winning here
                if (!IsThereSpaceToMove())
                    CheckWin();

            }
        }
    }

    public void CheckWin()
    {
        if (levelManager.AllPointsCollected())
        {
            Debug.Log("It's A Win");
            gameManager.OnDone();
        }
        else
        {
            gameManager.OnRetry();
            Debug.Log("You Lost");
        }
    }

    bool IsThereSpaceToMove()
    {
        int up = GetMovableBlocks(transform.up);
        int down = GetMovableBlocks(-transform.up);
        int right = GetMovableBlocks(transform.right);
        int left = GetMovableBlocks(-transform.right);

        if (up == 0 && down == 0 && left == 0 && right == 0)
            return false;
        else 
            return true;
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
            blocks = GetMovableBlocks(dir);
            finalPos = transform.position;

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

        points.SetActive(false);

        hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity);
        if (hit.collider != null)
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

        points.SetActive(true);

        return (blocks < 0) ? (blocks + 1) : (blocks - 1);
    }
}
