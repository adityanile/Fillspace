using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Max points to be collected by moving the snake to win the level
    [SerializeField]
    private int maxPoints;
    private Transform spawanPos;

    public GameObject pencil;

    public int takenPoints;

    public GameObject pointsPref;

    public GameObject points;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitialiseInSequence());
    }

    IEnumerator InitialiseInSequence()
    {
        yield return new WaitForSeconds(0.1f);
        points = Instantiate(pointsPref, new Vector2(0, 0), pointsPref.transform.rotation, gameObject.transform);
        
        yield return new WaitForSeconds(0.1f);
        maxPoints = points.transform.childCount;
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

    public bool AllPointsCollected()
    {
        if(takenPoints == maxPoints)
            return true;
        else
            return false; 
    }
}
