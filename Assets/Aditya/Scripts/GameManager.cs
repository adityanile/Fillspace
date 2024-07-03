using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> paintings;

    public GameObject currentPainting;
    public GameObject lastPainting;

    public GameObject currentPref;

    private int index;

    // Ui changes here
    public GameObject nextBtn;
    public GameObject retryBtn;

    public float animTime = 2f;
    public GameObject confettiEffect;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;

        ShowNextPainting();
    }

    public void OnRetry()
    {
        retryBtn.SetActive(true);
    }

    // Both side buttons management is done here
    public void OnDone()
    {
        confettiEffect.SetActive(true);
        StartCoroutine(AfterAnimation());
    }

    public void OnClickRetry()
    {
        DestroyImmediate(currentPainting, true);
        currentPainting = Instantiate(currentPref, new Vector2(0, 0), currentPref.transform.rotation);

        retryBtn.SetActive(false);
    }

    public void OnClickNext()
    {
        nextBtn.SetActive(false);

        ShowNextPainting();
    }

    IEnumerator AfterAnimation()
    {
        yield return new WaitForSeconds(animTime);

        nextBtn.SetActive(true);
        confettiEffect.SetActive(false);
    }

    void ShowNextPainting()
    {
        if (index < paintings.Count)
        {
            lastPainting = (index == 0) ? null : currentPainting;
            currentPainting = paintings[index];

            currentPref = currentPainting;
            currentPainting = Instantiate(currentPainting, new Vector2(0, 0), Quaternion.identity);

            if (lastPainting)
                DestroyImmediate(lastPainting, true);

            index++;
        }
        else
        {
            Debug.Log("Game Completed");
            SceneManager.LoadScene(0);
        }
    }
}
