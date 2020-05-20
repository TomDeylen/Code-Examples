using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    Transform mTransform;
    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 4f;
    public float ySmooth = 4f;
    public Vector2 minXAndY;
    public Vector2 maxXAndY;

    void Start()
    {
        mTransform = transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        float targetX = mTransform.position.x;
        float targetY = mTransform.position.y;

        if(CheckXMargin())
             targetX = Mathf.Lerp(targetX, target.position.x, Time.deltaTime * xSmooth);
        if(CheckYMargin())
            targetY = Mathf.Lerp(targetY, target.position.y, Time.deltaTime * ySmooth);

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        mTransform.position = new Vector3(targetX, targetY, mTransform.position.z);

    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - target.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - target.position.y) > yMargin;
    }
}
