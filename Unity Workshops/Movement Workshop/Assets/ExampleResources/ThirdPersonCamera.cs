using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	public Transform target;
	Vector3 offset;
	Vector3 angleOffset;

	public float cameraMoveSmoothing = 0.2f;
	public float cameraRotateSmoothing = 0.2f;
	Vector3 cameraMoveSpeed;
	Vector3 cameraRotateSpeed;

	void Awake () {
		ResetOffsets ();
	}

	void FixedUpdate () {
        if (target == null)
            return;

		Vector3 desiredPosition = target.position - target.rotation * offset;
		Quaternion desiredRotation = target.rotation * Quaternion.Euler (angleOffset);

		transform.position = Vector3.SmoothDamp (transform.position,
												desiredPosition,
												ref cameraMoveSpeed,
												cameraMoveSmoothing);
		transform.rotation = SmoothlyRotate (transform.rotation, 
												desiredRotation, 
												ref cameraRotateSpeed, 
												cameraRotateSmoothing);
		//transform.LookAt (target);
	}
		
	void ResetOffsets()
	{
		if (target == null)
			return;

		offset = target.position - transform.position;

		Quaternion directionDifference = Quaternion.FromToRotation (target.forward, transform.forward);

		angleOffset = directionDifference.eulerAngles;

	}

	Quaternion SmoothlyRotate(Quaternion from, Quaternion to, ref Vector3 velocity, float smoothTime)
	{
		Vector3 desiredAngle = to.eulerAngles;
		Vector3 currentAngle = from.eulerAngles;
		Vector3 newAngle = new Vector3 ();
		newAngle.x = Mathf.SmoothDampAngle (currentAngle.x, desiredAngle.x, ref velocity.x, smoothTime);
		newAngle.y = Mathf.SmoothDampAngle (currentAngle.y, desiredAngle.y, ref velocity.y, smoothTime);
		newAngle.z = Mathf.SmoothDampAngle (currentAngle.z, desiredAngle.z, ref velocity.z, smoothTime);

		return Quaternion.Euler (newAngle);
	}


}

