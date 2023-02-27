using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent _newMeshAgent;
    Camera mainCam;

    void Start()
    {
        _newMeshAgent = GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            // ALL keys must be named 'key' and a num ("Key0")
            int keyNum = Int32.Parse(other.name.Substring(3)); 
            Destroy(other.gameObject);
            PublicVars.hasKey[keyNum] = true;
        }
    }
}
