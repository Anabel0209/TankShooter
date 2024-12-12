using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    //Variable to set
    public TMP_Text timerText;

    private float timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        //time counter
        timeElapsed += Time.deltaTime;

        //calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        //format the string
        string timeFormated = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeFormated;
    }
}
