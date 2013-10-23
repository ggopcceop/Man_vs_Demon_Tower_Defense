using UnityEngine;
using System.Collections;

using Pathfinding;

public class ChareterHandler : MonoBehaviour {
	
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
	
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
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
	void FixedUpdate () {
		if (path == null) {            
			//We have no path to move after yet            
			return;        
		}                
		if (currentWaypoint >= path.vectorPath.Count) {         
			Debug.Log ("End Of Path Reached");           
			return;      
		}               
		//Direction to the next waypoint  
		Vector3 targetVelocity  = (path.vectorPath[currentWaypoint]-transform.position).normalized;        
		targetVelocity *= speed;
		
		Vector3 velocity = rigidbody.velocity;
	    Vector3 velocityChange = (targetVelocity - velocity);
	    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	    velocityChange.y = 0;
	    rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		
		Debug.Log("Moved 1");
		
		//Check if we are close enough to the next waypoint     
		//If we are, proceed to follow the next waypoint      
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {    
			currentWaypoint++;           
			return;        
		}
	}
	
	public void move(Vector3 to){
		seeker.pathCallback += OnPathComplete;
		seeker.StartPath(transform.position, to);
	}
	
	public void OnDisable () {   
		seeker.pathCallback -= OnPathComplete;
	} 
}
