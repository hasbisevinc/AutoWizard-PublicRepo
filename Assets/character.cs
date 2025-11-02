using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class character : MonoBehaviour
{

    public Joystick joystick;

    public float speed = 5f;
   

    private void Update()
    {
        float x = joystick.Horizontal * speed;  
    }
}