using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	IGameState currState;
	IGameState gamePlayState = new GamePlayState();
	IGameState menuState = new MenuState();
	
	// Use this for initialization
	void Start () 
	{
		currState = menuState;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currState.GS_Update ();
		
	}
	
	public void ChangeState(IGameState newState)
	{
		
		currState.GS_Exit ();
		
		currState = newState;
		
		currState.GS_Enter ();
	}
	

	public void AddScore(int points)
	{
		GamePlayState state = (GamePlayState)currState;
		state.AddScore (points);
	}

	public void AddBalls(int numBalls)
	{
		GamePlayState state = (GamePlayState)currState;
		state.AddBalls (numBalls);
	}

	public void BallLost()
	{
		AddBalls (1);
		GameObject ball = GameObject.Find ("Ball");
		Vector3 newPos = new Vector3(-9.25f,
		                             0.5f,
		                             10.0f);

		ball.transform.position = newPos;
		ball.transform.rigidbody.velocity = Vector3.zero;
	}

	// TODO: Find a better spot for this. Preferrably somewhere in the GamePlayState.
	public void TargetDropped(int targetID)
	{
		GamePlayState state = (GamePlayState)currState;
		state.TargetDropped (targetID);
	}

	public void SetHighScore(int hscore)
	{
		GamePlayState state = (GamePlayState)currState;
		state.SetHighScore(hscore);
	}

	public void StartGame()
	{
		ChangeState (gamePlayState);
	}
	

}
