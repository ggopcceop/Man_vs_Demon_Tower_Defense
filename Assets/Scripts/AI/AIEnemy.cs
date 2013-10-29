using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pathfinding;

public class AIEnemy : MonoBehaviour {
	
	public GameObject primaryTarget;
	public GameObject secondaryTarget;
	
	//The calculated path    
	public Path path;
	private Seeker seeker;
	
	//The AI's speed per second    
	public float speed = 1;     
	
	public float maxVelocityChange = 10.0f;

	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint    
	public float nextWaypointDistance = 3;     
	
	//The waypoint we are currently moving towards    
	private int currentWaypoint = 0;
	
	private CharacterController controller;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
		
		move();
	}
	
	public void OnPathComplete (Path p) {        
		Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);        
		if (!p.error) {           
			path = p;            
			//Reset the waypoint counter            
			currentWaypoint = 0;       
		}    
	}
	
	
	// Update is called once per frame
	void Update () {
		if (path == null) {            
			//We have no path to move after yet            
			return;        
		}                
		if (currentWaypoint >= path.vectorPath.Count) {         
			Debug.Log ("End Of Path Reached"); 
			path = null;
			return;      
		}               
		//Direction to the next waypoint  
		Vector3 targetVelocity  = (path.vectorPath[currentWaypoint]-transform.position).normalized;        
		targetVelocity *= speed;
		
		controller.Move(targetVelocity * Time.deltaTime);
		
		//Check if we are close enough to the next waypoint     
		//If we are, proceed to follow the next waypoint      
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {    
			currentWaypoint++;           
			return;        
		}
	}
	
	public void move(){
		seeker.pathCallback += OnPathComplete;
		seeker.StartPath(transform.position, secondaryTarget.transform.position);
	}
	
	public void OnDisable () {   
		seeker.pathCallback -= OnPathComplete;
	} 
}
