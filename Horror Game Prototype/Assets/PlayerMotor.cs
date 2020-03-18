using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    Camera cam;
    public float mouseSensitivity = 2.0f;
    float rotatePitch;
    float pitchRange = 60.0f;
    float rotateYaw;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        rotateYaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotatePitch += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotatePitch = Mathf.Clamp(rotatePitch, -pitchRange, pitchRange);
        cam.transform.localRotation = Quaternion.Euler(rotatePitch, rotateYaw, 0);

    }
}
