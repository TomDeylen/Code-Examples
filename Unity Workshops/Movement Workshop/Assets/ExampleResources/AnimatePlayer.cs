using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    Animator anim;
    float forwardPressed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        forwardPressed = Input.GetAxis("Vertical");
        forwardPressed = Mathf.Abs(forwardPressed);
        anim.SetFloat("Forward", forwardPressed);
    }
}
