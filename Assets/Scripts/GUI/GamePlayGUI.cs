using UnityEngine;
using System.Collections;

public class GamePlayGUI : MonoBehaviour {

	public GUISkin customSkin;
	//Player variables
	public int healthCount = 10;
	public int scoreCount = 0;
	public int cashCount = 200;
	public int waveLevel = 1;
	
	public GUIText waveText;
	public GUIText healthText;
	public GUIText scoreText;
	public GUIText cashText;

	//
	public GUITexture Start_button;
	public GUITexture tower1_GUItexture;
	public GUITexture tower2_GUItexture;
	public GUITexture tower3_GUItexture;
	public GUITexture menu_GUItexture;
	//towers cost    
	public int tower1Cost=10;
	public int tower2Cost=20;
	public int tower3Cost=30;
	private string condition;

	private GameWareControler gameWareControler;
	private GameControl gameControl;

	//toolbarInt
	//public int toolbarInt = 0;
	//
	//private string[] toolbarStrings = new string[] {"Menu","Retry","Next"};
	// Use this for initialization
	void Start () {
		//UpdateGUI();
		gameWareControler = GameObject.Find("GameWareControler").GetComponent<GameWareControler>();
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
	}
	
	void Update(){
		Rect rect = new Rect ((-Screen.width / 2), (Screen.height / 2) - 85, 45, 45);
		Start_button.pixelInset = rect;

		rect = new Rect ((-Screen.width / 2), (-Screen.height / 2) , 45, 45);
		tower1_GUItexture.pixelInset = rect;

		rect = new Rect ((-Screen.width / 2) + 50, (-Screen.height / 2) , 45, 45);
		tower2_GUItexture.pixelInset = rect;

		rect = new Rect ((-Screen.width / 2) + 100, (-Screen.height / 2) , 45, 45);
		tower3_GUItexture.pixelInset = rect;

		rect = new Rect ((Screen.width / 2)-45, (Screen.height / 2)-85 , 45, 45);
		menu_GUItexture.pixelInset = rect;

		waveText.text = "Wave: "+gameWareControler.currentWave;
		scoreText.text = "Score: "+scoreCount;
		healthText.text = "Lives: "+healthCount;
		cashText.text = "Coin: "+cashCount;
		condition = cashText.text+"  "+waveText.text+"  "+healthText.text+"  "+scoreText.text;

	}


	void OnGUI(){
		GUI.skin = customSkin;
		GUI.Box(new Rect (0,0,Screen.width,40),condition);
		GUI.Label(new Rect(1,28,45,100),"Click to Start");
		GUI.Label(new Rect(Screen.width-45,28, 45, 45), "Menu");
		GUI.Label(new Rect(5,Screen.height-65,250,50),"$10   $20    $30");

		//Check for lose game state
		if (gameControl.currentGameState == GameControl.GameState.Lose) {
			//GUI window shows lose screen
			GUI.Box(new Rect(Screen.width/12,Screen.height/7,Screen.width/12*10,Screen.height/7*5),"YOU LOSE");
			//toolbarInt = GUI.Toolbar(new Rect((Screen.width)/12*2,(Screen.height)/7*4,370,50),toolbarInt,toolbarStrings);
			//GUI texture
			Rect rect = new Rect ((-Screen.width / 2)+50, (-Screen.height / 4) , 45, 45);
			menu_GUItexture.pixelInset = rect;
		}
		else if (gameControl.currentGameState == GameControl.GameState.Win) {
			//GUI window shows win screen
			GUI.Box(new Rect(Screen.width/12,Screen.height/7,Screen.width/12*10,Screen.height/7*5),"YOU WIN");
		}

	}

}
