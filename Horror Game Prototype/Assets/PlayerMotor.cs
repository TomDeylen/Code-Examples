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
    Transform cTransform;
    LayerMask layerMask;
    public Transform controllerAsset;
    float controllerMaxRotation = 30f;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cTransform = cam.transform;
        layerMask = LayerMask.GetMask("Zone");
       
    }

    // Update is called once per frame

    public void CameraMovement(float mouseX, float mouseY)
    {
        rotateYaw += mouseX * mouseSensitivity;
        rotatePitch += -mouseY * mouseSensitivity;
        rotatePitch = Mathf.Clamp(rotatePitch, -pitchRange, pitchRange);
        cTransform.localRotation = Quaternion.Euler(rotatePitch, rotateYaw, 0);
        CheckZones();
        if (PlayerController.Instance.CurrentZone == Zone.TVZone)
            controllerAsset.rotation = Quaternion.Slerp(controllerAsset.rotation, Quaternion.Euler(0, controllerAsset.rotation.y, controllerAsset.rotation.z), .2f);
        else
            controllerAsset.rotation = Quaternion.Slerp(controllerAsset.rotation, Quaternion.Euler(controllerMaxRotation, controllerAsset.rotation.y, controllerAsset.rotation.z), .2f);
    }

    void CheckZones()
    {
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(cTransform.position, cTransform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if(hit.transform.name == "TVZone")
            {
                PlayerController.Instance.SetZone(Zone.TVZone);
            }
        }
        else
        {
            PlayerController.Instance.SetZone(Zone.NoZone);
        }
    }
}
