﻿using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public bool level1 = false;
	public bool level2 = false;
	public bool level3 = false;
	public bool menu = false;
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
		if (level1)
			Application.LoadLevel(3);
		else if (level2)
			Application.LoadLevel (4);
		else if (level3)
			Application.LoadLevel (5);
		else if (menu){
			Application.LoadLevel(0);
		}
	}
}
