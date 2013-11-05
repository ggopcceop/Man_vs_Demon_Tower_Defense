using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	
	public enum PlayerState {Normal, Building};
	
	public enum GameState {Starting, Playing};
	
	public PlayerState currentPlayerState;
	public GameState currentGameState;
	
	
	// Use this for initialization
	void Start () {
	 	currentGameState = GameState.Starting;
		currentPlayerState = PlayerState.Normal;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
