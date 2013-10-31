using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AITower : MonoBehaviour {
	
	//the arrow, magic shoot by the tower
	public GameObject ejection;
	
	//time interval between shooting
	public float ejectionInterval = 5;
	
	//the range of the tower
	public float range = 50;
	
	GameControler gameControler;
	
	//the eject point of the tower
	Transform eject;	

	// Use this for initialization
	void Start () {
		
		gameControler = GameObject.Find("GameControler").GetComponent<GameControler>();
		
		eject = transform.FindChild("eject");
	}
	
	float currentInterval = 0;
	// Update is called once per frame
	void Update () {		
		if(currentInterval > ejectionInterval){
			Shoot();
		} else {
			currentInterval += Time.deltaTime;
		}
	}
	
	void Shoot(){
		LinkedList<GameObject> enemies = gameControler.AIEnemies;
		foreach(GameObject o in enemies){
			float dist = Vector3.Distance(o.transform.position, transform.position);
			if(dist < range){
				Debug.Log(string.Format("Tower: {0} find a enemy {1}", name, o.name));
				GameObject clone = Instantiate(ejection, eject.position, Quaternion.identity) as GameObject;
				AIEjection ej = clone.GetComponent<AIEjection>();
				ej.target = o;
				
				Physics.IgnoreCollision(collider, clone.collider);
				
				currentInterval = 0;
				break;
			}
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
