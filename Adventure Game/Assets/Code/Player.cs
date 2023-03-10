using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    NavMeshAgent _newMeshAgent;
    Camera mainCam;
    AudioSource _audiosource;
    public AudioClip smallcatcall;
    public AudioClip bigcatcall;
    public AudioClip shootSound;
    public AudioClip hitSound;
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
    public string currentLevel;

    int lifeCounter; // player gets total 3 life
    public GameObject heartIcon;
    private List<GameObject> totalLife = new List<GameObject>(); // array to hold heart images
    // public GameObject deadSound;
    public string endGame = "GameOver";

    GameManager _gameManager;
    GameObject heart1;
    GameObject heart2;
    GameObject heart3;
    Renderer _renderer;

    void Start()
    {
        lifeCounter = 3;
        _newMeshAgent = GetComponent<NavMeshAgent>();
        PublicVars.playcatcalls = true;
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        heart1 = GameObject.FindGameObjectWithTag("CatHeart1");
        heart2 = GameObject.FindGameObjectWithTag("CatHeart2");
        heart3 = GameObject.FindGameObjectWithTag("CatHeart3");
        mainCam = Camera.main;
       _audiosource = GetComponent<AudioSource>();
       _renderer = GetComponent<Renderer>();
    //    for(int i = 0; i<3; i++){
    //         float gap = i*0.7f;
    //         totalLife.Add(Instantiate(heartIcon, new Vector3(470.4f+gap,147f, 0), Quaternion.identity));
    //     }
       StartCoroutine(Catcalls());
    }

    IEnumerator FlashRed() {

       Color originalColor = _renderer.material.GetColor("_Color");
        yield return new WaitForSeconds(10);
        _renderer.material.shader = Shader.Find("_Color");
        _renderer.material.SetColor("_Color", Color.green);

        //Find the Specular shader and change its Color to red
        _renderer.material.shader = Shader.Find("Specular");
        _renderer.material.SetColor("_SpecColor", originalColor);
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
        if(other.CompareTag("Yarn")) {
            _gameManager.AddYarn();
        }
        if (other.CompareTag("Key"))
        {
            if (levelToLoad == "victory") {
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
        if(other.CompareTag("Enemy"))
        {
            _audiosource.PlayOneShot(hitSound); 
            StartCoroutine(FlashRed());  
            // If no more heart left, then the player is dead.
            if (lifeCounter == 1) {
                Destroy(heart1);
                _gameManager.resetScore();
               SceneManager.LoadScene(endGame);
            } else {
                // Instantiate(deadSound, transform.position, Quaternion.identity); // dead sound per collision
                if(lifeCounter == 3) {
                    Destroy(heart3);
                }
                else if(lifeCounter == 2) {
                    Destroy(heart2);
                }
                lifeCounter = lifeCounter - 1; // when a zombie hits, lose 1 life
                // totalLife.Clear();
                // for(int i = 0; i<lifeCounter; i++){
                //     float gap = i*0.7f;
                //     totalLife.Add(Instantiate(heartIcon, new Vector3(216+gap,-50.43f, 0), Quaternion.identity));
                // }
            }
        }
        if(other.CompareTag("BigLion"))
        {
            _audiosource.PlayOneShot(hitSound);   
            // If no more heart left, then the player is dead.
                Destroy(heart3);
                Destroy(heart2);
                Destroy(heart1);
               SceneManager.LoadScene(endGame);
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
