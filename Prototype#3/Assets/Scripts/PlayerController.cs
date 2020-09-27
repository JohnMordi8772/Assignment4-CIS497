using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator playerAnimator;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public float gravityMod;
    public float jumpForce;
    public ForceMode jumpForceMode;

    public bool isOnGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set reference variables to components
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        //Start running
        playerAnimator.SetFloat("Speed_f", 1.0f);

        rb = GetComponent<Rigidbody>();
        if (Physics.gravity.y > -10)
        {
            Physics.gravity *= gravityMod;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, jumpForceMode);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerAnimator.SetTrigger("Jump_trig");
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            Debug.Log("Game Over!");
            dirtParticle.Stop();
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            gameOver = true;
        }
    }
}
