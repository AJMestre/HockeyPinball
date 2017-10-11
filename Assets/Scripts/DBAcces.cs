using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


public class DBAcces : MonoBehaviour {

	void AddScoreToDB(int score)
	{

		string connectionString = "URI=file:" + Application.dataPath + "/scoresDB.db";
		IDbConnection dbcon;
		dbcon = (IDbConnection)new SqliteConnection (connectionString);
		
		dbcon.Open (); // Open connection to database
		
		IDbCommand dbcmd = dbcon.CreateCommand ();
		
		string sql = "INSERT INTO scores VALUES (" + score.ToString () + ")";
		
		dbcmd.CommandText = sql;

		dbcmd.ExecuteNonQuery ();

		dbcmd.Dispose ();
		dbcmd = null;
		dbcon.Close ();
		dbcon = null;

	}

	void LoadDB()
	{
		string connectionString = "URI=file:" + Application.dataPath + "/scoresDB.db";
		IDbConnection dbcon;
		dbcon = (IDbConnection)new SqliteConnection (connectionString);
		
		dbcon.Open (); // Open connection to database
		
		IDbCommand dbcmd = dbcon.CreateCommand ();
		
		string sql = "SELECT score " + "FROM scores";
		
		dbcmd.CommandText = sql;
		
		IDataReader reader = dbcmd.ExecuteReader ();
		
		int HighScore = 0;
		int Score = 0;
		while(reader.Read()) 
		{
			Score = reader.GetInt32(0);
			Debug.Log (HighScore);
			if(Score > HighScore)
				HighScore = Score;
			
		}
		
		GameObject.Find ("MainGame").SendMessage ("SetHighScore", HighScore);
		
		reader.Close ();
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbcon.Close ();
		dbcon = null;
	}
}
