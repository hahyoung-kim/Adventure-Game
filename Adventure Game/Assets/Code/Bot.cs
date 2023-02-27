using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot : MonoBehaviour
{
    NavMeshAgent _newMeshAgent;
    GameObject player;

    void Start()
    {
         _newMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer()
    {
        while(true){
            yield return new WaitForSeconds(1f);
            _newMeshAgent.destination = player.transform.position;
        }
    }  

}
