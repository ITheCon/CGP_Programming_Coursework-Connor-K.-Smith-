﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    Vector3 cameraSpacing;
    // Start is called before the first frame update
    void Start()
    {
        cameraSpacing = (transform.position - player.transform.position)*1.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Will follow the player object, but keep moving further away as the player increases in size and mass
        transform.position = player.transform.position + cameraSpacing*(player.GetComponentInParent<Rigidbody>().mass*0.5f);
    }
}
