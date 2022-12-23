using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform camPivot;
    public Transform camera;

    private Vector3 camF;
    private Vector3 camR;

    public Rigidbody rb;

    public float jumpForce;
    public float boostSpeed;

    public bool isGrounded;
    public bool canMove = true;

    float headingX;
    float headingY;
    
    public float maxFallDistance;

    public GameObject currentCheckpoint;

    public List<GameObject> enemiesDefeated;

    public Material baseColor;
    public Material airDashColor;

    public bool canChangeColor = true;

    public float velocity;
    public AudioSource jumpNoise;
    public AudioSource airDashNoise;
    public Animator anim;
    public float camSens;

    [Header("Movement")]
    public float speed;
    public bool wontTakeDamage;
    public float cameraFacingX;
    public float cameraFacingY;
    public float health = 5.0f;
    Vector2 inputMov;
    public float currentSpeed;
    private bool airDashing;

    private float gravityTimer;

    public GameObject airDashParticles;

    [Header("Jump Stuff")]
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public bool canAirDash = true;
    public Vector3 addedForce;

    [Header ("Wall jumping Stuff")]
    
    public Vector3 hitSpot;
    public bool wallJumping;
    public bool onWall;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        headingX += Input.GetAxis("Mouse X") * Time.deltaTime * camSens;
        headingY += Input.GetAxis("Mouse Y") * Time.deltaTime * camSens;

        headingY = Mathf.Clamp(headingY, -80f, 80f);
        camPivot.rotation = Quaternion.Euler(-headingY, headingX, 0);

        inputMov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        camF = camera.forward;
        camR = camera.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        boostSpeed = dashSpeed(boostSpeed);
        boostSpeed = Mathf.Clamp(boostSpeed, 150, 350);

        gravityTimer += 1*Time.deltaTime;
        
        if(inputMov.x != 0 && inputMov.y != 0)
        {
            speed = 1750;
            rb.drag = 2;
        }else if (inputMov.x == 0 && inputMov.y == 0 && isGrounded)
        {
            rb.drag = 7;
        }else
        {
            speed = 2500;
            rb.drag = 2;
        }
        
        if(gravityTimer > 0.5)
        {
            GetComponent<Gravity>().enabled = true;
        }
        if(!canAirDash)
        {
            airDashParticles.GetComponent<ParticleSystem>().Play(true);
        }else
        {
            airDashParticles.GetComponent<ParticleSystem>().Stop(true);
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {   
                rb.AddForce(Vector3.up*jumpForce*Time.deltaTime + addedForce*40, ForceMode.Impulse);
                isJumping = true;
                jumpTimeCounter = jumpTime;
                jumpNoise.Play ();
            }else if(onWall && !isGrounded)
            {
                wallJumping = true;
                jumpNoise.Play ();
                StartCoroutine("WallJump");
            }else
            {
                wallJumping = false;
            }    
        }

        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                //rb.velocity = Vector3.up * jumpForce;    
                rb.AddForce(Vector3.up*jumpForce * Time.deltaTime + addedForce, ForceMode.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }else
            {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canAirDash)
        {
            StartCoroutine("AirDash");
            canMove = false;
            canAirDash = false;
            airDashing = true;
            airDashNoise.Play();
        }
        cameraFacingX = camPivot.rotation.x;
        cameraFacingY = camPivot.rotation.y;

        if(Input.GetKeyDown(KeyCode.E) && canAirDash)
        {
            StartCoroutine("UpwardsAirDash");
            canMove = false;
            canAirDash = false;
            airDashing = true;
            airDashNoise.Play();
        }

        if(health < 1)
        {
            transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
            StartCoroutine("CanMoveAgain");
            foreach(GameObject enemy in enemiesDefeated)
            {
                enemy.SetActive(true);
                enemy.GetComponent<Turret>().canShoot = true;
            }
            enemiesDefeated.Clear();
            health = 5;
        }
        if(transform.position.y < -maxFallDistance || Input.GetKeyDown(KeyCode.R))
        {
            canMove = false;
            GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeScreen>().Fade();
            StartCoroutine("CanMoveAgain");
            health = 5;
            foreach(GameObject enemy in enemiesDefeated)
            {
                enemy.SetActive(true);
                enemy.GetComponent<Turret>().canShoot = true;
            }
            enemiesDefeated.Clear();
        }

        if(canAirDash == false && canChangeColor)
        {
            GetComponent<Renderer>().material = airDashColor;
        }else if (canChangeColor && canAirDash == true)
        {
            GetComponent<Renderer>().material = baseColor;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            foreach(GameObject enemy in enemiesDefeated)
            {
                enemy.SetActive(true);
                enemy.GetComponent<Turret>().canShoot = true;
            }
            enemiesDefeated.Clear();
        }
    }


    float dashSpeed(float speed)
    {
        float result;
        if(camPivot.localEulerAngles.x > 250)
        {
            result = 350 - ( -(camPivot.localEulerAngles.x - 360) * 3 );
        }else
        {
            result = 350 - (camPivot.localEulerAngles.x * 3);
        }

        return result;
    }
    void FixedUpdate()
    {
        float targetSpeedX = inputMov.x * speed;
        float targetSpeedY = inputMov.y * speed;
        //velocity = Mathf.Abs(Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z));
        //float wantedVel = 29;
        
        if(canMove)
        {   
            rb.AddForce((camF * speed * inputMov.y + camR*speed*inputMov.x) *Time.deltaTime);

            //transform.position += (camF * inputMov.y + camR * inputMov.x) * Time.deltaTime * speed;
            //rb.MovePosition(transform.position + (camF * inputMov.y + camR * inputMov.x) * speed * Time.deltaTime);
            //rb.AddForce(new Vector3(inputMov.x * speed * camF.x, 0, inputMov.x * speed*camF.x));
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "plat")
        {
            //transform.parent = coll.transform;
            //rb.velocity = coll.gameObject.GetComponent<Rigidbody>().velocity;
        }else
        {
            addedForce = Vector3.zero;
        }
        if(coll.gameObject.layer == 7)
        {    
            if(!isGrounded && wontTakeDamage || airDashing)
            {
                coll.gameObject.SetActive(false);
                enemiesDefeated.Add(coll.gameObject);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(Vector3.up*1500 * Time.deltaTime, ForceMode.Impulse);
                canAirDash = true;
                
            }
            else if(!wontTakeDamage && !airDashing)
            {
                health -= 1.0f;
            }
        }
        if(coll.gameObject.layer == 8)
        {
            canMove = false;
            transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
            StartCoroutine("CanMoveAgain");
            foreach(GameObject enemy in enemiesDefeated)
            {
                enemy.SetActive(true);
                enemy.GetComponent<Turret>().canShoot = true;
            }
            enemiesDefeated.Clear();
            health = 5;
        }
        //If player is in the air and collide with an object that is off the ground and at
        // a set angle, onWall is set to true and find the hitspot
        if (!isGrounded && coll.contacts[0].normal.y < 0.1f)
        {

            onWall = true;
            hitSpot = new Vector3(coll.contacts[0].normal.x, 0, coll.contacts[0].normal.z);

            //Once space is pressed, wallJumping will be equal to true, and onWall is false
            if(wallJumping)
            {               
                onWall = false;
            }
            Debug.DrawRay(coll.contacts[0].point, coll.contacts[0].normal, Color.red, 1.25f);

        }
        if(coll.gameObject.tag == "plat")
        {
            //Change the Player's parent to the moving Platform
            transform.parent = coll.transform;

            rb.interpolation = RigidbodyInterpolation.None;

            //Extra force being added to the jump
            addedForce = coll.gameObject.GetComponent<MovingPlatform>().lastMoveDirection*40 * Time.deltaTime;
        }else
        {
            transform.parent = null;
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {

        //If player is in the air and collide with an object that is off the ground and at
        // a set angle, onWall is set to true and find the hitspot
        if (!isGrounded && collision.contacts[0].normal.y < 0.1f)
        {

            onWall = true;
            hitSpot = new Vector3(collision.contacts[0].normal.x, 0, collision.contacts[0].normal.z);

            //Once space is pressed, wallJumping will be equal to true, and onWall is false
            if(wallJumping)
            {               
                onWall = false;
            }
            Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red, 1.25f);

        }
        if(collision.gameObject.tag == "plat")
        {
            //Change the Player's parent to the moving Platform
            transform.parent = collision.transform;

            rb.interpolation = RigidbodyInterpolation.None;

            //Extra force being added to the jump
            addedForce = collision.gameObject.GetComponent<MovingPlatform>().lastMoveDirection*40 * Time.deltaTime;
        }else
        {
            transform.parent = null;
        }
        
        if(collision.gameObject.tag == "plat")
        {
            //Change the Player's parent to the moving Platform
            transform.parent = collision.transform;

            //Extra force being added to the jump
            addedForce = collision.gameObject.GetComponent<MovingPlatform>().lastMoveDirection*40 * Time.deltaTime;
        }else
        {
            transform.parent = null;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag == "plat")
        {
            transform.parent = null;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
        if(!isGrounded)
        {
            onWall = false;
        }
    }


    IEnumerator CanMoveAgain()
    {
        
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        canMove = true;
    }
    
    IEnumerator AirDash()
    {
        float time = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isJumping = false;

        gravityTimer = 0;
        GetComponent<Gravity>().enabled = false;
        while(time < 0.3)
        {
            //rb.velocity = new Vector3(-camPivot.transform.rotation.x, -camPivot.transform.rotation.y, camPivot.transform.rotation.z) * 10;
            //transform.position += (camera.forward) * Time.deltaTime * boostSpeed;
            rb.AddForce(camera.forward * Time.deltaTime * boostSpeed, ForceMode.Impulse);
            time += 1*Time.deltaTime;
            yield return null;
        }
        canMove = true;
        
        yield return new WaitForSeconds(0.3f);

        airDashing = false;

        yield return null;
    }

    IEnumerator UpwardsAirDash()
    {
        float time = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isJumping = false;

        gravityTimer = 0;
        GetComponent<Gravity>().enabled = false;
        while(time < 0.3)
        {
            //rb.velocity = new Vector3(-camPivot.transform.rotation.x, -camPivot.transform.rotation.y, camPivot.transform.rotation.z) * 10;
            //transform.position += (camera.forward) * Time.deltaTime * boostSpeed;
            rb.AddForce(Vector3.up * Time.deltaTime * 150.0f, ForceMode.Impulse);
            time += 1*Time.deltaTime;
            yield return null;
        }
        canMove = true;    
        yield return new WaitForSeconds(0.3f);
        airDashing = false;

        yield return null;
    }

    IEnumerator WallJump()
    {
        //Set canMove to false and disable gravity temprarily
        float t = 0;
        canMove = false;
        gravityTimer = 0;
        GetComponent<Gravity>().enabled = false;

        //For 0.2 seconds, add a force towards where the camera is facing.
        while(t < 0.2)
        {
            rb.AddForce((Vector3.up*125 + hitSpot.normalized*jumpForce)*Time.deltaTime, ForceMode.Impulse);
            t += 1*Time.deltaTime;
            yield return null;
        }
        //Wait 0.3 more seconds before turning gravity back on and turning canMove
        //to true and wallJumping to false
        canMove = true;
        wallJumping = false;
        yield return null;
    }
}
