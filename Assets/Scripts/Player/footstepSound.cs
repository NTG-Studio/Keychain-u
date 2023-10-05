using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSound : MonoBehaviour
{
    [SerializeField] private AudioSource left_Foot;

    [SerializeField] private AudioSource right_foot;
    
    
    public void leftFoot()
    {
        left_Foot.Play();
    }

    public void rightFoot()
    {
        right_foot.Play();
    }
}
