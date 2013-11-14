using UnityEngine;
using System.Collections;

public class GamePlayGUI : MonoBehaviour {
	
	//Player variables
	private int healthCount = 10;
	private int scoreCount = 0;
	private int cashCount = 200;
	private int waveLevel = 1;
	public Texture towerImage1;
	public Texture towerImage2;
	public Texture towerImage3;
	private string condition;
	//GUI variables
	public GUIText waveText;
	public GUIText healthText;
	public GUIText scoreText;
	public GUIText cashText;
	public GUISkin customSkin;
	//tower cost              - or array -
	private int towerCost;
	//public Rect windowRect = new Rect(10,Screen.height -60,200,200);
	// Use this for initialization
	void Start () {
		UpdateGUI();
		condition = cashText.text+"  "+waveText.text+"  "+healthText.text+"  "+scoreText.text;
	}
	
	// Update is called once per frame
	void UpdateGUI () {
		waveText.text = "Wave: "+waveLevel;
		scoreText.text = "Score: "+scoreCount;
		healthText.text = "Lives: "+healthCount;
		cashText.text = "Coin: "+cashCount;
	}
	
	void OnGUI(){
		GUI.skin = customSkin;
		//string button1 = GUI.Button (new Rect(10,Screen.height -50,50,50),towerImage1);
		
		GUI.Box(new Rect (0,0,Screen.width,Screen.height),condition);
		/*
		//windowRect = GUI.Window(0,windowRect,doWindow,"Towers");
		if(GUI.Texture(new Rect(10,Screen.height-51,50,50),towerImage1)){
			Debug.Log("tower 1");
			
		}
		if(GUI.Texture (new Rect(65,Screen.height -51,50,50),towerImage2)){
			Debug.Log("tower 2");
		}
		if(GUI.Texture(new Rect (120,Screen.height - 51,50,50),towerImage3)){
			Debug.Log("tower 3");
		}*/
		
	}
	/*
	void doWindow(int windowID){
		if(GUI.Button (new Rect(10,Screen.height-51,50,50),towerImage1)){
			Debug.Log("tower 1");
			
		}
		if(GUI.Button (new Rect(65,Screen.height -51,50,50),towerImage2)){
			Debug.Log("tower 2");
		}
		if(GUI.Button(new Rect (120,Screen.height - 51,50,50),towerImage3)){
			Debug.Log("tower 3");
		}
	}*/
	
	
	
	
	
}
