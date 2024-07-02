using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Max points to be collected by moving the snake to win the level
    private int maxPoints;
    private Transform spawanPos;

    public GameObject pencil;

    public int takenPoints;

    // Start is called before the first frame update
    void Start()
    {
        BringPlayer();
    }


    void BringPlayer()
    {
        spawanPos = transform.GetChild(0);
        Instantiate(pencil, spawanPos.position, pencil.transform.rotation, gameObject.transform);
    }

    public void PointTaken()
    {
        takenPoints++;
    }
}
