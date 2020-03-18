using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Controller[] controllers;
    public Inputs inp;


    void Start()
    {
        
    }

    void Update()
    {
        inp = new Inputs();
        inp.horizontal = Input.GetAxis("Horizontal");
        inp.vertical = Input.GetAxis("Vertical");
        inp.jump = Input.GetButton("Jump");
        inp.grapple = Input.GetButton("Fire2");

        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].Tick(Time.deltaTime, inp);
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].FixedTick(Time.deltaTime);
        }
    }
}

[System.Serializable]
public class Inputs
{
    public float vertical;
    public float horizontal;
    public bool jump;
    public bool grapple;
}
