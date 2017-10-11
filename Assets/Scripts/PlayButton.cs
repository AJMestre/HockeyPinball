using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public string inputName = "Start";

	void MUpdate()
	{
		if (Input.GetButton (inputName)) 
		{
			Vector3 newPos = new Vector3(1000.0f, 0.0f, 1000.0f);
			gameObject.transform.position = newPos;
			GameObject.Find ("MainGame").SendMessage("StartGame");
		}
	}
	
}
