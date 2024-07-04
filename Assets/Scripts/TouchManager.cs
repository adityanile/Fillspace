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
        // Managing the Touch Controls
        HandleTouchControl();

        // Managing Keyboard Controls
        HandleKeyboardControls();
    }

    void HandleKeyboardControls()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Right Swipe
            playerManager.MoveBlock(transform.right);
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Left Swipe
            playerManager.MoveBlock(-transform.right);
            sr.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // UP Swipe
            playerManager.MoveBlock(transform.up);
            sr.flipY = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Down Swipe
            playerManager.MoveBlock(-transform.up);
            sr.flipY = false;
        }
    }
    public void HandleTouchControl()
    {
        // Control for Touch Enabled Devices
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
                            OnRightSwipe();
                        }
                        else
                        {   //Left swipe
                            OnLeftSwipe();
                        }
                    }
                    else
                    {
                        if (lp.y > fp.y)
                        {   //Up swipe
                            OnUpSwipe();
                        }
                        else
                        {   //Down swipe
                            OnDownSwipe();
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

    void OnRightSwipe()
    {
        playerManager.MoveBlock(transform.right);
        sr.flipX = true;
    }
    void OnLeftSwipe()
    {
        playerManager.MoveBlock(-transform.right);
        sr.flipX = false;
    }
    void OnUpSwipe()
    {
        playerManager.MoveBlock(transform.up);
        sr.flipY = true;
    }
    void OnDownSwipe()
    {
        playerManager.MoveBlock(-transform.up);
        sr.flipY = false;
    }

}
