using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding;

public class AIEnemy : MonoBehaviour {
	
	public GameObject primaryTarget;
	public GameObject secondaryTarget;
	
	CharacterControl characterControl;
	
	// Use this for initialization
	void Start () {
		characterControl = GetComponent<CharacterControl>();
	}	
	
	bool moved = false;
	// Update is called once per frame
	void Update () {
		if(!moved){
			characterControl.move(secondaryTarget.transform);
			moved = true;
		}
	}
}
