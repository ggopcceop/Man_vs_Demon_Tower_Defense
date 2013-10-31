using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float maxHealth;
	private float currentHealth;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void getDamage(float amount){
		currentHealth-=amount;
		if(currentHealth <= 0){
			currentHealth = 0;
			die();
		}
	}
	
	void die(){
	}
}
