using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	//the type of damage the game had
	public enum DamageType {Physis, Magic, Explosion};
	//the type of damage effect the game had
	public enum DamageEffect {None, Slow, Bleeding};
	
	public static void doDamage(GameObject target, float damage){
		doDamage(target, DamageEffect.None, damage);
	}
	
	public static void doDamage(GameObject target, DamageEffect effect, float damage){
		switch(effect){
		case DamageEffect.None:
			Character chara = target.GetComponent<Character>();
			chara.GetDamage(damage);
			break;
		default:
			break;
		}
	}
	
	public static void doDamage(Vector3 location, DamageType type, DamageEffect effect, float damage){
		switch(type){
		case DamageType.Physis:
			break;
		default:
			break;
		}
	}
}
