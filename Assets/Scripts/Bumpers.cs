using UnityEngine;
using System.Collections;

public class Bumpers : MonoBehaviour {

	public float power = 0.0f;
	public bool poppedUp = true;
	public int targetID = -1;

	private GameObject mainGame;

	// Use this for initialization
	void Start () 
	{
		mainGame = GameObject.Find ("MainGame");
	}
	
	// Update is called once per frame
	// TODO: Make popUp targets event based.
	void Update () 
	{
		if (gameObject.tag == "PopUp")
		{
			if (poppedUp == true) 
			{
				Vector3 newPos = new Vector3(gameObject.transform.position.x,
				                             0.5f,
				                             gameObject.transform.position.z);
				gameObject.transform.position = newPos;
			}
			else
			{
				Vector3 newPos = new Vector3(gameObject.transform.position.x,
				                             -0.5f,
				                             gameObject.transform.position.z);
				gameObject.transform.position = newPos;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		other.rigidbody.AddExplosionForce (power, this.transform.position, 5.0f);
	}

	void OnCollisionExit(Collision other)
	{
		if (gameObject.tag == "PopUp")
		{
			if (poppedUp == true) 
			{
				mainGame.SendMessage ("AddScore", 20);
				mainGame.SendMessage("TargetDropped", targetID);
				poppedUp = false;
			}
		}
		else if(gameObject.tag == "Goal")
		{
			mainGame.SendMessage ("AddScore", 200);
		}
		else
		{
			mainGame.SendMessage ("AddScore", 10);
		}


	}

	void PopUpTarget()
	{
		poppedUp = true;
	}
	
}
