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
    public GameObject bulletPrefab;


    void Start()
    {
        _newMeshAgent = GetComponent<NavMeshAgent>();
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
            newBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
            // Destroy(newBullet, 3);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            PublicVars.playcatcalls = false;
            stop_playing = true;
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
            if (!stop_playing)    
            _audiosource.PlayOneShot(smallcatcall);            
        }
    }
}
