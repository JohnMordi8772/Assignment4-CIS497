/** John Mordi* 
 * Assignment #4 Challenge #3* 
 * Manages win/loss conditions and on-screen text*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static float score;
    public static bool gameOver;//replaced gameOver in other scripts to keep it to one variable
    public bool won;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOver = false;
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)//Keeps scoreText consistently right
        {
            scoreText.text = "Score: " + score;
        }
        if(score >= 10)//marks a win and gameOver
        {
            gameOver = true;
            won = true;
        }
        if(gameOver && won)
        {
            scoreText.text = "You win!\nPress R to play again!";
        }
        else if(gameOver)
        {
            scoreText.text = "You lose.\nPress R to try again.";
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
