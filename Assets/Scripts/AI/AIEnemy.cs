using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding;

public class AIEnemy : MonoBehaviour {
	
	public GameObject primaryTarget;
	public GameObject secondaryTarget;

	public float attickInterval = 3;

	public float distance;
	
	CharacterControl characterControl;
	
	// Use this for initialization
	void Start () {
		characterControl = GetComponent<CharacterControl>();
	}	

	float currentInterval = 0;

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, secondaryTarget.transform.position);
		if(distance > 1){
			characterControl.move(secondaryTarget.transform);
		} else {
			if (currentInterval > attickInterval) {
				characterControl.AttackTarget(secondaryTarget);
				currentInterval = 0;
			}
			currentInterval += Time.deltaTime;
		}
	}
}
