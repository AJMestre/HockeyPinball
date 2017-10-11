using UnityEngine;
using System.Collections;

public class LaunchSpring : MonoBehaviour {

	public string inputName = "Pull";
	public float maxDistance = 50.0f;
	public float speed = 1.0f;
	public float power = 2000.0f;
	public GameObject ball;

	bool ready = false;
	bool fire = false;
	float currDistance = 0.0f;

	// Use this for initialization
	void Start () 
	{
		ball = GameObject.Find ("Ball");
	}

	void GPUpdate()
	{
		if (Input.GetButton (inputName)) 
		{
			if (currDistance < maxDistance) 
			{
				Vector3 trans = new Vector3 (0, 0, speed * Time.deltaTime);
				transform.Translate (trans);
				currDistance += speed * Time.deltaTime;
				fire = true;
			}
		} 
		else if (currDistance > 0) 
		{
			if(fire == true && ready == true)
			{
				Vector3 force = new Vector3(0, 0, currDistance * power);
				ball.transform.TransformDirection (Vector3.forward*10);
				ball.rigidbody.AddForce (force);
				fire = false;
				ready = false;
			}
			
			currDistance -= 20 * Time.deltaTime;
			
			if(currDistance <= 0.0f)
			{
				currDistance = 0.0f;
				fire = false;
			}
			
			Vector3 trans = new Vector3(0, 0, -20 * Time.deltaTime);
			transform.Translate(trans);
			
			if(currDistance == 0.0f)
			{
				Vector3 newPos = new Vector3(transform.position.x,
				                             transform.position.y,
				                             14.0f);
				transform.position = newPos;
			}
			
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Ball") 
		{
			ready = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Ball") 
		{
			ready = false;
		}
	}
}
