using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    private bool canSetTrue = true;

    private PlayerMovement playerMov;
    private GameObject player;
    private Vector3 validDir = Vector3.up;

    public GameObject groundParticles;
    public AudioSource landingSound;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMov.canAirDash == false && canSetTrue == false)
        {
            StartCoroutine("SetAirDashTrue");
        }
        if(playerMov.isGrounded)
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Calculate the position of the top of the GameObject where the player needs to land
        float positionToReach = other.transform.position.y + (other.transform.localScale.y/2) - other.transform.eulerAngles.x;
        //If the player's position is greater than posToReach and its ground, then do the code
        //if(other.gameObject.layer == 6 && transform.position.y > positionToReach)
        //{
            //Gets the color of the material attached to the GameObject and sets the particleSystems
            //color to it
            //groundParticles.GetComponent<ParticleSystem>().startColor = other.GetComponent<Renderer>().material.color;
            //groundParticles.GetComponent<ParticleSystem>().Play(true);
            //Play landing sound
            //landingSound.Play ();
            //Stop Airdash Noise
            //playerMov.airDashNoise.Stop();
        //}
        if (other.gameObject.layer == 6)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1) && !playerMov.isGrounded)
            {
                // Collided with a surface facing mostly upwards
                playerMov.isGrounded = true;
                groundParticles.GetComponent<ParticleSystem>().startColor = other.gameObject.GetComponent<Renderer>().material.color;
                groundParticles.GetComponent<ParticleSystem>().Play(true);
                playerMov.onWall = false;
                playerMov.wallJumping = false;
                playerMov.wontTakeDamage = false;
                playerMov.canAirDash = true;
                canSetTrue = false;
                //Play landing sound
                landingSound.Play();
                //Stop Airdash Noise
                playerMov.airDashNoise.Stop();
            }

        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1))
            {
                playerMov.canAirDash = true;
                playerMov.isGrounded = true;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                playerMov.canAirDash = false;
            }
        }
        if(other.gameObject.layer == 7)
        {
            playerMov.wontTakeDamage = true;
            playerMov.canAirDash = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            playerMov.isGrounded = false;
            if(playerMov.canAirDash == true)
            {
                player.GetComponent<Gravity>().enabled = true;
            }
        }
        
    }

    IEnumerator SetAirDashTrue()
    {
        yield return new WaitForSeconds(0.3f);
        canSetTrue = true;
        yield return null;
    }
}
