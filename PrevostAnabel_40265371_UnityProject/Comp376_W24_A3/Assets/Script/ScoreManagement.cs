using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagement : MonoBehaviour
{
    //Variables to set
    public int score;
    public float timeInterval = 1f;
    public TMP_Text scoreText;
    public TMP_Text scoreTextGameOver;

    private float timeSinceLastIncrement = 0f;

    //method used to add to the current score 
    public void UpdateScore(int points)
    {
        score += points;
    }

    // Update is called once per frame
    void Update()
    {
        //displat the current score on the screen
        scoreText.SetText("Score: " + score.ToString());
        scoreTextGameOver.SetText("Score: " + score.ToString());
        //timer
        timeSinceLastIncrement += Time.deltaTime;

        //increment score each seconds
        if(timeSinceLastIncrement >= timeInterval)
        {
            score++;
            timeSinceLastIncrement = 0f;
        }
    }

    //if collecting a bonus point item
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bonusPoints"))
        {
            UpdateScore(10);
            Destroy(collision.gameObject);
        }
    }
}
