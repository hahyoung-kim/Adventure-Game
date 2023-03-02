using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;

    Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Quaternion newRot = transform.rotation;
        // newRot.y = transform.rotation.z + 0.4f;
        // transform.rotation = newRot;
        // offset = transform.position - player.transform.position;
    }

    private void LateUpdate() {
        // transform.position = player.transform.position + offset;
        Vector3 newPos = transform.position;
        newPos.x = player.transform.position.x;
        newPos.z = player.transform.position.z - 5;
        transform.position = newPos;
        transform.LookAt(player.transform);
    }
}
