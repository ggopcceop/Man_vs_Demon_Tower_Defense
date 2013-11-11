using UnityEngine;
using System.Collections;

public class GamePlayGUI : MonoBehaviour {
	
	//Player variables
	private int healthCount = 10;
	private int scoreCount = 0;
	private int cashCount = 200;
	private int waveLevel = 1;
	
	//GUI variables
	public UILabel waveText;
	public UILabel healthText;
	public UILabel scoreText;
	public UILabel cashText;
	
	//tower cost              - or array -
	private int towerCost;
	
	// Use this for initialization
	void Start () {
		UpdateGUI();
	
	}
	
	// Update is called once per frame
	void UpdateGUI () {
		waveText.text = "Wave: "+waveLevel;
		scoreText.text = "Score: "+scoreCount;
		healthText.text = "Lives: "+healthCount;
		cashText.text = "Coin: "+cashCount;
	}
}
