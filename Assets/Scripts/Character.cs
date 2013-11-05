using UnityEngine;
using System.Collections;



public class Character : MonoBehaviour
{
	public enum CharacterType {Player, Enemy};
	
	
	public string name;
	public CharacterType type;
	public float maxHealth;
	private float currentHealth;
	private bool die = false;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
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
		if(type == CharacterType.Enemy){
			GameWareControler game = GameObject.Find("GameWareControler").GetComponent<GameWareControler>();
			game.AIEnemies.Remove(gameObject);
		}
		
	}
	
	public bool IsDie ()
	{
		return die;
	}
}
