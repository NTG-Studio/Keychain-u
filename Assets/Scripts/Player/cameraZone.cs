using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZone : MonoBehaviour
{
    [SerializeField] public GameObject affectedCamera;
    [SerializeField] private cameraManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<cameraManager>();
        if (manager == null)
        {
            Debug.Log("Warning Could Not Find Camera Manager (did you forget to add one to the scene?)");
        }
    }
}
