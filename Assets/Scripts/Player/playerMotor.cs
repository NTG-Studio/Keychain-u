using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector2 input;
    [SerializeField] private float walkSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private string walkingBoolName = "walking";
    [SerializeField] private Vector3 gravity;
    [SerializeField] private float turnSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateInput();
        turn();
        updateVelocity();
        applyVelocity();
        anim.SetBool(walkingBoolName,input.y > 0.1f);
    }

    void updateInput()
    {
        input=sys_input.instance.input;
    }

    void turn()
    {
        transform.Rotate(0,turnSpeed * Time.deltaTime * input.x,0);
    }

    void updateVelocity()
    {
        velocity  = input.y * walkSpeed * transform.forward * Time.deltaTime;
    }

    void applyVelocity()
    {
        velocity += gravity;
        controller.Move(velocity);
        velocity = Vector3.zero;
    }
    
}
