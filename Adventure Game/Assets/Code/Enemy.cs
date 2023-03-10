using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    public NavMeshAgent enemy;
    public string enemytype;
    AudioSource _audiosource;
    public AudioClip dogwhimper;
    public AudioClip dogbark;
    public AudioClip lionroar;
    public AudioClip lionwhimper;
    MeshRenderer _renderer;
    GameObject player;
    Color origcolor;
    bool move = true;
    bool playbark = true;
    bool playroar = true;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _audiosource = GetComponent<AudioSource>();
        _renderer = GetComponent<MeshRenderer>();
        origcolor = _renderer.material.color;
        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer(){
        while (true) {
            //yield return new WaitForSeconds(1);
            if (move) {
                if (Vector3.Distance(player.transform.position, transform.position) < 15) {
                    enemy.destination = player.transform.position;
                }
                yield return new WaitForSeconds(3);
                if (enemytype == "dog") {
                    if(playbark) {
                        _audiosource.PlayOneShot(dogbark);
                    }
                }
                else if (enemytype == "lion") {
                    if(playroar) {
                        _audiosource.PlayOneShot(lionroar);
                    }
                }
            }
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (other.CompareTag("Fence"))
        {
            Destroy(other);
        }
        if (other.CompareTag("Yarn"))
        {
            if (enemytype == "dog") {
                playbark = false;
                _audiosource.PlayOneShot(dogwhimper);
            }
            else if (enemytype == "lion") {
                playroar = false;
                _audiosource.PlayOneShot(lionwhimper);
            }
            // StartCoroutine(FlashRed());
            StartCoroutine(Stop());
        }
    }

    IEnumerator Stop(){
        move = false;
        yield return new WaitForSeconds(1);
        move = true;
        if(enemytype == "dog") {
            playbark = true;
        }
        else if (enemytype == "lion") {
            playroar = true;
        }
    }

    // IEnumerator FlashRed(){
    //     _renderer.material.color = Color.red;
    //     yield return new WaitForSeconds(5);
    //     _renderer.material.color = origcolor;
    //     }
}
