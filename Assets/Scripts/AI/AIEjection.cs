using UnityEngine;
using System.Collections;

public class AIEjection : MonoBehaviour {
	//the target of the ejection
	public GameObject target;
	
	//the damage of the ejection
	public float damage;
	//the flying speed
	public float flyingSpeed;
	//the max range of ejection can fly
	public float range;
	
	//can the ejection track the target
	public bool canTracking;
	
	//is the ejection has splash damage
	public bool hasSplash;
	public float splashArea;
	public float splashDegradation;
	
	Vector3 targetPoint; 
	
	// Use this for initialization
	void Start () {
		targetPoint = target.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target == null){
			Destroy(gameObject);
		}
		
		if(canTracking){
			targetPoint = target.transform.position;
		}
		
		
		Vector3 dirction = targetPoint - transform.position;
		dirction *= flyingSpeed;
		collider.transform.Translate(dirction * Time.deltaTime);
	}
	
}
