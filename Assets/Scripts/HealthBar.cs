using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour {

	//the texture of full and empty health bar
	public Texture fullHealth;
	public Texture emptyHealth;

	//the size and position of health bar
	public float healthBarLength = 50;
	public float healthBarHeight = 7;
	public float healthBarHightOffset = 20;

	GameObject player;
	GameWareControler gameWare;
	Camera mainCamera;
	GameObject castle;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("GameControl").GetComponent<GameControl>().player;
		gameWare = GameObject.Find ("GameWareControler").GetComponent<GameWareControler>();
		mainCamera = GameObject.Find ("Camera").camera;	
		castle = gameWare.castle;
	}

	// draw health bar of each entity in game.
	void OnGUI (){
		//draw player's health bar
		float maxHealth = player.GetComponent<Character>().maxHealth;
		float currentHealth = player.GetComponent<Character>().currentHealth;

		DrawHealthBar(player.transform, currentHealth, maxHealth);

		//draw enemy's health bar
		LinkedList<GameObject> enemies = gameWare.AIEnemies;
		foreach (GameObject o in enemies) {
			if(!o.GetComponent<Character>().IsDie()){
				maxHealth = o.GetComponent<Character>().maxHealth;
				currentHealth = o.GetComponent<Character>().currentHealth;
				DrawHealthBar(o.transform, currentHealth, maxHealth);
			}
		}


		//draw health bar of castle
		maxHealth = castle.GetComponent<Character>().maxHealth;
		currentHealth = castle.GetComponent<Character>().currentHealth;
		DrawHealthBar(castle.transform, currentHealth, maxHealth);
		
	}

	// draw the health bar for each entity
	void DrawHealthBar(Transform pos, float currentHealth, float maxHealth){
		//calculate the position of health bar
		Vector3 drawPos = mainCamera.WorldToScreenPoint(pos.position);
		drawPos.x -= healthBarLength / 2;
		drawPos.y = Screen.height - drawPos.y - healthBarHightOffset;

		//calculate the length of the full health bar
		float currentHealthLength = (healthBarLength / maxHealth) * currentHealth;

		//draw empty health bar at the buttom
		GUI.DrawTexture(new Rect(drawPos.x, drawPos.y, healthBarLength, healthBarHeight), emptyHealth);

		//draw full health bar in the front
		GUI.DrawTexture(new Rect(drawPos.x, drawPos.y, currentHealthLength, healthBarHeight), fullHealth);
	}
}
