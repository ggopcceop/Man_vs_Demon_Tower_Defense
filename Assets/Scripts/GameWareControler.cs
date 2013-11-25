using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameWareControler : MonoBehaviour
{
	/** data structure to store wares data */
	public struct Ware
	{
		public float time;
		public string name;
		public int count;

		public Ware (float time, string name, int count)
		{
			this.time = time;
			this.name = name;
			this.count = count;
		}
		
		// Override the ToString method:
		public override string ToString ()
		{
			return(string.Format ("{0} {1} {2}", time, name, count));   
		}
		
	}
	
	/* store all enemies ai in the game */
	public LinkedList<GameObject> AIEnemies = new LinkedList<GameObject> ();
	
	/* store all the wares informations */
	public LinkedList<Ware> gameWares = new LinkedList<Ware> ();
	
	//the spawn point of enemy
	public GameObject enemySpawn;
	
	//the human castle for enemy target
	public GameObject castle;
	
	//the wares config file of this level
	public string wareConfigFileName;

	public int currentWave=1;
	//count the total time passed
	float gameTime = 0;
	
	//the next ware of enemies
	Ware nextWare;
	
	GameControl gameControl;
	
	// Use this for initialization
	void Start ()
	{
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		
		//read the ware config
		ReadWareConfig ();
		
		//read a ware from the list
		if (gameWares.Count > 0) {
			nextWare = gameWares.First.Value;
			gameWares.RemoveFirst ();
		} else {
			spawning = false;
		}
	}
	
	//for testing the ware if is spawning
	bool spawning = true;
	
	// Update is called fixed frame
	void FixedUpdate ()
	{
		if(gameControl.currentGameState == GameControl.GameState.Playing){
			//add time passed
			gameTime += Time.deltaTime;
			
			//if spawning a ware
			if (spawning && gameTime > nextWare.time) {
				SpawnWare (nextWare);
				
				if (gameWares.Count > 0) {
					nextWare = gameWares.First.Value;
					gameWares.RemoveFirst ();
				} else {
					spawning = false;
				}
			}
		}
	}
	
	void ReadWareConfig ()
	{
		FileInfo theSourceFile = new FileInfo ("Assets" + Path.DirectorySeparatorChar + wareConfigFileName);

		StreamReader reader = theSourceFile.OpenText ();
		string text;
		while ((text = reader.ReadLine())!= null) {
			string[] split = text.Split (':');
			if (split.Length == 3) {
				Ware data = new Ware ();
				data.time = float.Parse (split [0]);
				data.name = split [1];
				data.count = int.Parse (split [2]);
				
				Debug.Log (data);
				
				gameWares.AddLast (data);
			}
		}
	}
	
	void SpawnWare (Ware ware)
	{
		for (int i = 0; i < ware.count; i++) {				
			GameObject prelab = Resources.Load (ware.name) as GameObject;
			GameObject ai = Instantiate (prelab, enemySpawn.transform.position, Quaternion.identity) as GameObject;

			ai.GetComponent<AIEnemy> ().secondaryTarget = castle;
			AIEnemies.AddLast (ai);				
		}
	}
}
