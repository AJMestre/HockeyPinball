using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;


public class GamePlayState : IGameState {


	public int score = 0;
	public int balls = 1;
	public int[] droppedTargets = new int[4];
	public bool popUpBonus = true;

	private int HighScore = 0;
	private GameObject mainGame;
	private GameObject launchSpring;
	private GameObject leftPaddle;
	private GameObject rightPaddle;
	private GameObject rightPaddle2;
	private GameObject[] popUps;

	public override void GS_Enter()
	{
		GameObject.Find ("MainGame").SendMessage ("LoadDB");
		score = 0;
		launchSpring = GameObject.Find ("LaunchSpring");
		leftPaddle = GameObject.Find ("LeftPaddle");
		rightPaddle = GameObject.Find ("RightPaddle");
		rightPaddle2 = GameObject.Find ("RightPaddle2");
		popUps = GameObject.FindGameObjectsWithTag("PopUp");
		mainGame = GameObject.Find ("MainGame");
	}
	
	public override void GS_Exit()
	{
	
	}
	
	public override void GS_Update()
	{
		launchSpring.SendMessage ("GPUpdate");
		leftPaddle.SendMessage("GPUpdate");
		rightPaddle.SendMessage ("GPUpdate");
		rightPaddle2.SendMessage ("GPUpdate");

		// TODO: UI SHOULD NOT BE UPDATING EVERY FRAME. MAKE THIS EVENT BASED
		UILabel scoreLabel = GameObject.Find("ScoreLabel").GetComponent<UILabel> ();
		scoreLabel.text = score.ToString ();
		
		UILabel ballLabel = GameObject.Find ("BallLabel").GetComponent<UILabel> ();
		ballLabel.text = balls.ToString ();

		if (score > HighScore) 
		{
			HighScore = score;
		}

		UILabel HighScoreLabel = GameObject.Find("HighScoreLabel").GetComponent<UILabel> ();
		HighScoreLabel.text = HighScore.ToString();

		CheckToResetPopUps();

		if (Input.GetButton ("Cancel")) 
		{
			Application.Quit();
		}
	}

	public void AddScore(int points)
	{
		score += points;
	}

	public void AddBalls(int numBalls)
	{
		if (balls == 3) 
		{
			GameOver ();
			return;
		}
		balls += numBalls;
	}

	public void TargetDropped(int targetID)
	{
		droppedTargets [targetID] = 1;
		if (targetID > 0) 
		{
			if (droppedTargets [targetID - 1] != 1) 
			{
					popUpBonus = false;
			}
		}
		else
		{
			for(int i = 1; i < 4; i++)
			{
				if(droppedTargets[i] != 1)
				{
					popUpBonus = false;
				}
			}
		}
	}

	public void CheckToResetPopUps()
	{
		for (int i = 0; i < 4; i++) 
		{
			// if any drop target is not dropped, quit.
			if(droppedTargets[i] == 0)
			{
				break;
			}
			else 
			{
				if(i == 3) //If all targets were turned on
				{
					// Reset dropped targets to off position
					
					for(int j = 0; j < 4; j++)
					{
						droppedTargets[j] = 0;
						popUps[j].SendMessage("PopUpTarget");
					}
					
					if(popUpBonus == true)
					{
						AddScore(100);
					}
					else
					{
						AddScore (20);
						popUpBonus = true;
					}
					
				}
			}
		}
	}

	public void GameOver()
	{
		mainGame.SendMessage ("AddScoreToDB", score);
		balls = 1;
		score = 0;

		for(int i = 0; i < 4; i++)
		{
			droppedTargets[i] = 0;
			popUps[i].SendMessage("PopUpTarget");
		}

		if (score > HighScore) 
		{
			HighScore = score;
		}

	}

	public void SetHighScore(int hscore)
	{
		HighScore = hscore;
	}
	

	
}
