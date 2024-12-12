using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameStart : MonoBehaviour
{
    public GameObject startGameOverlay;
    public float timeDisplay;
    public TMP_Text countDown;
    public MainCanon myCanon;
    public GameObject pauseButton;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("in start");
        StartCoroutine(ShowDisplayForSeconds(timeDisplay));
    }

    // Update is called once per frame
    IEnumerator ShowDisplayForSeconds(float time)
    {
        pauseButton.SetActive(false);
        myCanon.inCutScene = true;
        Debug.Log("in coroutine");
        Time.timeScale = 0f;
        startGameOverlay.SetActive(true);

        float remainingTime = time;

        while (remainingTime > 0)
        {
            countDown.SetText("Starting in: " + remainingTime.ToString("F0"));
            remainingTime -= Time.unscaledDeltaTime;
            yield return null;
        }
        startGameOverlay.SetActive(false);
        myCanon.inCutScene = false;
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
