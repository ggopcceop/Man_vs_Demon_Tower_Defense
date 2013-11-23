using UnityEngine;
using System.Collections;

public class GamePlayGUI : MonoBehaviour {

	public GUISkin customSkin;
	//Player variables
	private int healthCount = 10;
	private int scoreCount = 0;
	private int cashCount = 200;
	private int waveLevel = 1;
	
	public GUIText waveText;
	public GUIText healthText;
	public GUIText scoreText;
	public GUIText cashText;
	//tower cost              - or array -
	public int tower1Cost;
	public int tower2Cost;
	public int tower3Cost;
	private string condition;
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

		
		GUI.Box(new Rect (0,0,Screen.width,40),condition);
	}

}
