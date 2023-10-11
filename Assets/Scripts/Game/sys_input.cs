using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class sys_input : MonoBehaviour
{
    [SerializeField] public Vector2 input=Vector2.zero;
    [SerializeField] public InputActionAsset moveAction;
    public static sys_input instance;

    public UnityEvent inventoryButton;
    public UnityEvent useButton;

    public UnityEvent up;
    public UnityEvent right;
    public UnityEvent left;
    public UnityEvent down;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        updateInput();   
    }

    void updateInput()
    {
        InputAction move = moveAction.FindAction("Move");
        input = move.ReadValue<Vector2>();
    }


    public void onUse(InputAction.CallbackContext context)
    {
        if (useButton != null)
        {
            useButton.Invoke();
        }
    }
    
    public void onInventory(InputAction.CallbackContext context)
    {
        if (inventoryButton != null)
        {
            inventoryButton.Invoke();
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        up.Invoke();
    }
    
    public void OnRight(InputAction.CallbackContext context)
    {
        right.Invoke();
    }
    
    public void OnLeft(InputAction.CallbackContext context)
    {
        left.Invoke();
    }
    
    public void OnDown(InputAction.CallbackContext context)
    {
        down.Invoke();
    }
}
