using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform joystick;
    public Transform button1;
    public Transform button2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void GetInputs(float horizontal)
    {
        joystick.localRotation = Quaternion.Euler(0, -horizontal * 12, 0);
    }
}
