using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent _newMeshAgent;
    Camera mainCam;
    AudioSource _audiosource;
    public AudioClip smallcatcall;
    public AudioClip bigcatcall;
    public AudioClip shootSound;
    bool stop_playing = false;
    int bulletSpeed = 5;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public Transform spawnPoint5;
    public Transform spawnPoint6;
    public GameObject bulletPrefab;
    public string levelToLoad;


    void Start()
    {
        _newMeshAgent = GetComponent<NavMeshAgent>();
        PublicVars.playcatcalls = true;
        
        mainCam = Camera.main;
       _audiosource = GetComponent<AudioSource>();
       StartCoroutine(Catcalls());
    }

    // Update is called once per frameo
    void Update()
    {
        // left click, 1 = right click
        if (Input.GetMouseButton(0)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit, 200))
            {
                print(hit.point);
                _newMeshAgent.destination = hit.point;
            }
        }
        if(Input.GetButtonDown("Jump")){
            if(PublicVars.shootingActivated) {
            _audiosource.PlayOneShot(shootSound);
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            GameObject newBullet2 = Instantiate(bulletPrefab, spawnPoint2.position, spawnPoint2.rotation);
            GameObject newBullet3 = Instantiate(bulletPrefab, spawnPoint3.position, spawnPoint3.rotation);
            GameObject newBullet4 = Instantiate(bulletPrefab, spawnPoint4.position, spawnPoint4.rotation);
            GameObject newBullet5 = Instantiate(bulletPrefab, spawnPoint5.position, spawnPoint5.rotation);
            GameObject newBullet6 = Instantiate(bulletPrefab, spawnPoint6.position, spawnPoint6.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
            newBullet2.GetComponent<Rigidbody>().velocity = spawnPoint2.forward * bulletSpeed;
            newBullet3.GetComponent<Rigidbody>().velocity = spawnPoint3.forward * bulletSpeed;
            newBullet4.GetComponent<Rigidbody>().velocity = spawnPoint4.forward * bulletSpeed;
            newBullet5.GetComponent<Rigidbody>().velocity = spawnPoint5.forward * bulletSpeed;
            newBullet6.GetComponent<Rigidbody>().velocity = spawnPoint6.forward * bulletSpeed;
            Destroy(newBullet, 3f);
            Destroy(newBullet2, 3f);
            Destroy(newBullet3, 3f);
            Destroy(newBullet4, 3f);
            Destroy(newBullet5, 3f);
            Destroy(newBullet6, 3f);

            // Destroy(newBullet, 3);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (levelToLoad == "level5" || levelToLoad == "victory") {
                if (PublicVars.hasKey[0] == true && PublicVars.hasKey[1] == true) {
                    PublicVars.playcatcalls = false;
                    stop_playing = true;
                }
            }
            else {
                stop_playing = true;
                PublicVars.playcatcalls = false;
            }
            // ALL keys must be named 'key' and a num ("Key0")
            int keyNum = Int32.Parse(other.name.Substring(3));
            Destroy(other.gameObject);
            PublicVars.hasKey[keyNum] = true;
        }
    }

    public IEnumerator Catcalls() {
        while(PublicVars.playcatcalls == true) {
            _audiosource.PlayOneShot(bigcatcall);   
            yield return new WaitForSeconds(5);    
            if (!stop_playing){
            _audiosource.PlayOneShot(smallcatcall); 
            }
        }
    }
}
