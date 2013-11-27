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
	
	//the spawn points of enemy
	public GameObject[] enemySpawn;

	//the human castle for enemy target
	public GameObject castle;
	
	//the wares config file of this level
	public string wareConfigFileName;

	public int currentWave = 0;

	//the time interval between 2 enemies
	public float enemyInterval = 1;

	//count the total time passed
	float gameTime = 0;
	
	//the next ware of enemies
	Ware nextWare;

	//set the time for next ware start to spawn
	float nextWareTime;
	
	GameControl gameControl;
	
	// Use this for initialization
	void Start ()
	{
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();

		if(enemySpawn.Length == 0){
			Debug.LogError("There is not spawn point for the enemy");
		}

		//read the ware config
		ReadWareConfig ();
		
		//read a ware from the list
		if (gameWares.Count > 0) {
			currentSpawning = false;
			nextWare = gameWares.First.Value;
			gameWares.RemoveFirst ();

			enemyCount = nextWare.count;
			
			nextWareTime = gameTime + nextWare.time;
		} else {
			spawning = false;
		}
	}
	
	//for testing the ware if is spawning
	bool spawning = true;

	//for testing the current ware if is spawing
	bool currentSpawning = false;
	//count number of enemies spawned in current ware 
	int enemyCount;
	//the interval from last enemy spawned
	float currentEnemyInterval;
	
	// Update is called every frame
	void Update ()
	{
		if(gameControl.currentGameState == GameControl.GameState.Playing){
			//add time passed
			gameTime += Time.deltaTime;

			if(currentSpawning){
				//spawn enemies in interval time
				if(currentEnemyInterval > enemyInterval){
					currentEnemyInterval = 0;
					SpawnWare(nextWare);
					enemyCount--;
				}

				//update time interval
				currentEnemyInterval += Time.deltaTime;

				//if spawning current ware is done
				if(enemyCount <= 0){
					//stop spawning current ware
					currentSpawning = false;

					//read the next ware
					if (gameWares.Count > 0) {	
						nextWare = gameWares.First.Value;
						gameWares.RemoveFirst ();
					}else{
						nextWare.count = 0;
					}

					//update the number of enemies in next ware
					enemyCount = nextWare.count;

					//set the time the nextware start to spawn
					nextWareTime = gameTime + nextWare.time;
				}
			}
			else {				
				//if is the time the spawn next ware
				if (spawning && gameTime > nextWareTime) {
					
					if (nextWare.count != 0) {						
						currentSpawning = true;
						currentWave++;
					} else {
						//if the count of next ware is 0 meaning there is
						//no more ware to spawn
						spawning = false;
					}
				}
			}
		}

		Character castleCharacter =  castle.GetComponent<Character>();
		if(castleCharacter.currentHealth <= 0){
			gameControl.currentGameState = GameControl.GameState.Lose;
		}

		//if there is not more wares and enemies, wid the game
		if(gameControl.currentGameState == GameControl.GameState.Playing && !spawning && AIEnemies.Count == 0){
			gameControl.currentGameState = GameControl.GameState.Win;
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

	//the index of the spawn points
	int spawnIndex = 0;

	void SpawnWare (Ware ware)
	{
		Transform spawnPoint = enemySpawn[++spawnIndex % enemySpawn.Length].transform;

		GameObject prelab = Resources.Load (ware.name) as GameObject;
		GameObject ai = Instantiate (prelab, spawnPoint.position, Quaternion.identity) as GameObject;

		ai.GetComponent<AIEnemy> ().secondaryTarget = castle;
		AIEnemies.AddLast (ai);				

	}
}
