using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject player;
    [SerializeField] public GameObject laserPrefab;

    public bool canShoot = true;
    public float rateOfFire;
    public float detectionRange;
    public float laserSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {    
        //Changes the laserPrefab's movement to go directly towards where the Turret is looking
        laserPrefab.GetComponent<Laser>().goTowards = transform.forward;

        //This checks the distance between the turret and the player and looks towards it if the player is in range
        //as well as starts shooting at the player
        if(Vector3.Distance(player.transform.position, transform.position) < detectionRange)
        {
            var rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.LookAt(player.transform);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*5000);
            if(canShoot)
            {
                StartCoroutine(ShootAtPlayer(rateOfFire));
                canShoot = false;
            }
        }
    }

    
    //Self explanatory shooting at player, Pew Pew
    IEnumerator ShootAtPlayer(float rOF)
    {
        yield return new WaitForSeconds(rOF);
        GameObject laserFired = Instantiate(laserPrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
        laserFired.GetComponent<Laser>().turret = gameObject;
        laserFired.GetComponent<Laser>().goTowards = transform.forward;
        laserFired.GetComponent<Laser>().speed = laserSpeed;
        canShoot = true;
        yield return null;
    }
}
