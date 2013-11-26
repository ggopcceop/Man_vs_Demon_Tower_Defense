using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public bool start = false;
	public bool exit = false;
	public bool story = false;
	public bool credits = false;
	public bool options = false;
	private bool showCredit =false;

	public GUISkin skin;
	//mouse enter, change color to black
	void OnMouseEnter(){
		//change the color of text to black
		renderer.material.color = Color.black;	
	}
	//mouse exit, change color to yellow
	void OnMouseExit(){
		//change the color of text to yellow
		renderer.material.color = Color.white;	
	}
	//mouse click on
	void OnMouseUp(){
		
		//playAgain is false, load level 1
		if (start) {
			Application.LoadLevel (1);
		}
		else if (story){
			//load story scene
			Application.LoadLevel(2);
		}
		else if (options){
		
		}
		else if (credits){
			showCredit = true;
		}
		else if (exit)
			Application.Quit();
	}

	void OnGUI(){

		GUI.skin = skin;
		if (showCredit){
			//GUI window
			GUI.Box(new Rect(Screen.width/12,Screen.height/7,Screen.width/12*10,Screen.height/7*5),"Credits");

			GUI.Label(new Rect (Screen.width/2-50,Screen.height/7*2,250,180),"Produced By \nChao Wang, \nJinyu Li, \nShuwen Ruan \nFalcuty of Compture Science ");


			if(GUI.Button(new Rect(Screen.width/2-45,Screen.height/7*5,90,45),"Back")){
				showCredit=false;
			}
		}
	}

}
