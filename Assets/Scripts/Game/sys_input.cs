using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game
{
    public class SysInput : MonoBehaviour
    {
        [SerializeField] public Vector2 input=Vector2.zero;
        [SerializeField] public InputActionAsset moveAction;
        public static SysInput Instance;

        public UnityEvent inventoryButton;
        public UnityEvent useButton;

        public UnityEvent up;
        public UnityEvent right;
        public UnityEvent left;
        public UnityEvent down;

        public UnityEvent rightShoulder;
        public UnityEvent leftShoulder;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            UpdateInput();   
        }

        void UpdateInput()
        {
            InputAction move = moveAction.FindAction("Move");
            input = move.ReadValue<Vector2>();
        }


        public void OnUse(InputAction.CallbackContext context)
        {
            if (useButton != null&& context.phase == InputActionPhase.Performed)
            {
                useButton.Invoke();
            }
        }
    
        public void OnInventory(InputAction.CallbackContext context)
        {
            if (inventoryButton != null && context.phase == InputActionPhase.Performed)
            {
                inventoryButton.Invoke();
            }
        }

        public void OnUp(InputAction.CallbackContext context)
        {
            if (up!=null &&context.phase == InputActionPhase.Performed)
            {
                up.Invoke();
            }
        }
    
        public void OnRight(InputAction.CallbackContext context)
        {
            if (right !=null &&context.phase == InputActionPhase.Performed)
            {
                right.Invoke();
            }
        }
    
        public void OnLeft(InputAction.CallbackContext context)
        {
            if (left !=null && context.phase == InputActionPhase.Performed)
            {
                left.Invoke();
            }
        }
    
        public void OnDown(InputAction.CallbackContext context)
        {
            if (down !=null && context.phase == InputActionPhase.Performed)
            {
                down.Invoke();
            }
        }
        
        public void OnRightShoulder(InputAction.CallbackContext context)
        {
            if (down !=null && context.phase == InputActionPhase.Performed)
            {
                rightShoulder.Invoke();
            }
        }
        
        public void OnLeftShoulder(InputAction.CallbackContext context)
        {
            if (down !=null && context.phase == InputActionPhase.Performed)
            {
                leftShoulder.Invoke();
            }
        }
    }
}
