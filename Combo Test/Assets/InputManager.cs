using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector2 joystickInput = new Vector2();

    void Start()
    {
        
    }


    void Update()
    {
        joystickInput.x = Input.GetAxis("Horizontal");
        joystickInput.y = Input.GetAxis("Vertical");

        if(Mathf.Abs(joystickInput.magnitude) < 0.002f)
        {
            joystickInput = Vector2.zero;
        }


    }


}