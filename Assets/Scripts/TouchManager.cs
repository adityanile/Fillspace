using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;

    private PlayerManager playerManager;
    public SpriteRenderer sr;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100;

        playerManager = GetComponent<PlayerManager>();
        sr = playerManager.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0); // get the touch

            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if ((lp.x > fp.x))
                        {   //Right swipe
                            playerManager.MoveBlock(transform.right);

                            sr.flipX = true;
                        }
                        else
                        {   //Left swipe
                            playerManager.MoveBlock(-transform.right);

                            sr.flipX = false;
                        }
                    }
                    else
                    {
                        if (lp.y > fp.y)
                        {   //Up swipe
                            playerManager.MoveBlock(transform.up);

                            sr.flipY = true;
                        }
                        else
                        {   //Down swipe
                            playerManager.MoveBlock(-transform.up);

                            sr.flipY = false;
                        }
                    }
                }
                else
                {
                    // Tap on the object
                }
            }
        }
    }
}
