using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] public GameObject affectedCamera;
    [SerializeField] private CameraManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<CameraManager>();
        if (manager == null)
        {
            Debug.Log("Warning Could Not Find Camera Manager (did you forget to add one to the scene?)");
        }
    }
}
