using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float restPosition = 0.0f;
	public float pressedPosition = 45.0f;
	public float strength = 10.0f;
	public float damper = 1.0f;
	public string inputName = "LeftPaddle";

	// TODO: MODIFY HINGE TO UPDATE ONLY WHEN BUTTON IS PRESSED OR RELEASED, NOT EVERY FRAME.
	void GPUpdate()
	{
		JointSpring spring = new JointSpring ();
		spring.spring = strength;
		spring.damper = damper;
		
		if (Input.GetButton (inputName))
			spring.targetPosition = pressedPosition;
		else
			spring.targetPosition = restPosition;
		
		hingeJoint.spring = spring;
		hingeJoint.useLimits = true;
		JointLimits limits = new JointLimits ();
		limits.min = restPosition;
		limits.max = pressedPosition;
		hingeJoint.limits = limits;
	}
}
