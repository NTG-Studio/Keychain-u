using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject currentCamera;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "camZone")
        {
            Debug.Log("hit " + other.name );
            cameraZone zone = other.gameObject.GetComponent<cameraZone>();
            if (currentCamera != null)
            {
                currentCamera.SetActive(false);
            }

            currentCamera = zone.affectedCamera;
            currentCamera.SetActive(true);
        }
    }
}
