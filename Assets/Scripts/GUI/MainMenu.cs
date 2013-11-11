using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public bool start = false;
	public bool exit = false;
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
		if (start)
			Application.LoadLevel(1);
		if (exit)
			Application.Quit();
	}
}
