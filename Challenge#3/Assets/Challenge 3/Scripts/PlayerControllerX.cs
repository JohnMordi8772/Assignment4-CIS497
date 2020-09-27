/** John Mordi* 
 * Assignment #4 Challenge #3* 
 * Allows the player to control their character and manages the use of particles and win/loss variables*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //public bool gameOver;

    public float floatForce;
    public ForceMode forceMode;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        //gameOver = false; replaced by ScoreManager

        if (Physics.gravity.y > -10f)//prevents gravity from getting stronger and stronger on restarts
        {
            Physics.gravity *= gravityModifier;
        }
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 15 && playerRb.velocity.y > 0)//prevents the player from going far above the screen
        {
            playerRb.velocity = Vector3.zero;
        }
        else
        {
            // While space is pressed and player is low enough, float up
            if (Input.GetKeyDown(KeyCode.Space) && !ScoreManager.gameOver)
            {
                playerRb.AddForce(Vector3.up * floatForce, forceMode);
            }
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            ScoreManager.gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            ScoreManager.score++;
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        else if(other.gameObject.CompareTag("Ground") && !ScoreManager.gameOver)//balloon bounces off the ground
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse); 
        }

    }

}
