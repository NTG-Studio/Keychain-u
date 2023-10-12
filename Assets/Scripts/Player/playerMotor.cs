using Game;
using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Vector3 velocity;
        [SerializeField] private Vector2 input;
        [SerializeField] private float walkSpeed;
        [SerializeField] private Animator anim;
        [SerializeField] private string walkingBoolName = "walking";
        [SerializeField] private Vector3 gravity;
        [SerializeField] private float turnSpeed;
    
        // Update is called once per frame
        void Update()
        {
            UpdateInput();
            Turn();
            UpdateVelocity();
            ApplyVelocity();
            anim.SetBool(walkingBoolName,input.y > 0.1f);
        }

        void UpdateInput()
        {
            input=SysInput.Instance.input;
        }

        void Turn()
        {
            transform.Rotate(0,turnSpeed * Time.deltaTime * input.x,0);
        }

        void UpdateVelocity()
        {
            velocity  = transform.forward * (input.y * walkSpeed * Time.deltaTime);
        }

        void ApplyVelocity()
        {
            velocity += gravity;
            controller.Move(velocity);
            velocity = Vector3.zero;
        }
    
    }
}
