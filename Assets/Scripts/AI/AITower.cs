using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AITower : MonoBehaviour
{
	
	//the arrow, magic shoot by the tower
	public GameObject ejection;
	
	//time interval between shooting
	public float ejectionInterval = 5;
	
	//the range of the tower
	public float range = 50;
	GameWareControler gameWareControler;
	
	//the eject point of the tower
	Transform eject;

	// Use this for initialization
	void Start ()
	{
		
		gameWareControler = GameObject.Find ("GameWareControler").GetComponent<GameWareControler> ();
		
		eject = transform.FindChild ("eject");
	}
	
	float currentInterval = 0;
	GameObject currentTarget = null;
	
	// Update is called once per frame
	void FixedUpdate ()
	{		
		if (currentInterval > ejectionInterval) {
			if(currentTarget != null){
				float dist = Vector3.Distance (currentTarget.transform.position, transform.position);
				if (dist > range) {
					currentTarget = null;
				}else if(currentTarget.GetComponent<Character>().IsDie()){
					currentTarget = null;
				}
			}
			if (currentTarget == null) {
				LinkedList<GameObject> enemies = gameWareControler.AIEnemies;
				foreach (GameObject o in enemies) {
					float dist = Vector3.Distance (o.transform.position, transform.position);
					if (dist < range) {
						Debug.Log (string.Format ("Tower: {0} find a enemy {1}", name, o.name));
						currentTarget = o;
						break;
					}
				}
			}
			if(currentTarget != null){
				Shoot ();
			}
		} 
		
		currentInterval += Time.deltaTime;
	}
	
	void Shoot ()
	{
		
		GameObject clone = Instantiate (ejection, eject.position, Quaternion.identity) as GameObject;
		AIEjection ej = clone.GetComponent<AIEjection> ();
		ej.target = currentTarget;
				
		Physics.IgnoreCollision (collider, clone.collider);
				
		currentInterval = 0;
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
