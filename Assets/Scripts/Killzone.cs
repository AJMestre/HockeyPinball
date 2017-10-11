using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour {

	void OnCollisionEnter()
	{
		GameObject.Find ("MainGame").SendMessage ("BallLost");
	}
}
