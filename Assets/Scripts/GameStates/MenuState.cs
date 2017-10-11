using UnityEngine;
using System.Collections;

public class MenuState : IGameState
{
	private GameObject startBackground;
	private GameObject startLabel;

	public override void GS_Enter()
	{
		Vector3 newPos = new Vector3 (0.0f, 0.0f, 0.0f);
		GameObject.Find ("StartBackground").transform.position = newPos;
		GameObject.Find ("StartLabel").transform.position = newPos;
	}
	
	public override void GS_Exit()
	{
		
	}
	
	public override void GS_Update()
	{
		GameObject.Find ("StartBackground").SendMessage ("MUpdate");
		GameObject.Find ("StartLabel").SendMessage ("MUpdate");

		if (Input.GetButton ("Cancel")) 
		{
			Application.Quit();
		}
	}

}
