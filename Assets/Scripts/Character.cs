using UnityEngine;
using System.Collections;


[RequireComponent (typeof (CharacterControl))]
public class Character : MonoBehaviour
{
	public enum CharacterType {Player, Tower, Enemy};
	
	
	public string name;
	public CharacterType type;
	public float maxHealth;
	
	private float currentHealth;
	private bool die = false;
	
	CharacterControl characterControl;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;	
		characterControl = GetComponent<CharacterControl>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(highlight){
			highlight = false;
		} else {
			renderer.material.SetColor("_Color", Color.white);
		}
	}
	
	public void GetDamage (float amount)
	{		
		currentHealth -= amount;
		
		Debug.Log (name + ": get damage " + amount + ";current health: " + currentHealth);
		
		if (currentHealth <= 0) {
			currentHealth = 0;
			Die ();
		}
	}
	
	void Die ()
	{
		Debug.Log (name + " died");
		die = true;
		characterControl.canMove = false;
		if(type == CharacterType.Enemy){
			GameWareControler game = GameObject.Find("GameWareControler").GetComponent<GameWareControler>();
			game.AIEnemies.Remove(gameObject);
		}
		
	}
	
	public bool IsDie ()
	{
		return die;
	}
	
	bool highlight = false;
	public void Highlight(){
		if(!die){
			highlight = true;
			if(type == CharacterType.Tower){
				renderer.material.SetColor("_Color", Color.green);
			}else if(type == CharacterType.Enemy){
				renderer.material.SetColor("_Color", Color.red);
			}
		}
	}
}
