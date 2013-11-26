using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof(Seeker))]
public class CharacterControl : AIPath {
	
	private Character character;
	
	/** Minimum velocity for moving */
	public float sleepVelocity = 0.4F;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		character = GetComponent<Character>();
		base.repathRate = 5;
	}
	
	protected new void Update () {
		
		//Get velocity in world-space
		Vector3 velocity;
		if (canMove) {
		
			//Calculate desired velocity
			Vector3 dir = CalculateVelocity (GetFeetPosition());
			
			//Rotate towards targetDirection (filled in by CalculateVelocity)
			if (targetDirection != Vector3.zero) {
				RotateTowards (targetDirection);
			}
			
			if (dir.sqrMagnitude > sleepVelocity*sleepVelocity) {
				//If the velocity is large enough, move
			} else {
				//Otherwise, just stand still (this ensures gravity is applied)
				dir = Vector3.zero;
			}
			
			if (navController != null)
				navController.SimpleMove (GetFeetPosition(), dir);
			else if (controller != null)
				controller.SimpleMove (dir);
			else
				Debug.LogWarning ("No NavmeshController or CharacterController attached to GameObject");

			velocity = controller.velocity;
			//Play animations

			Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
			float horizontalSpeed = horizontalVelocity.magnitude;

			if(animation != null && animation.enabled){
				if(horizontalSpeed > 0.1f){
					animation.Play("Walk");
				}else {
					animation.Play("Stand");
				}
			}


		} else {
			velocity = Vector3.zero;
		}
	}

	public void move(Transform target, bool force){
		if(this.target != target || force){
			this.target = target;
			if(force){
				base.SearchPath();
			}else{
				base.Start();
			}
		}
	}

	public void move(Transform target){
		move(target, false);
	}

	public void AttackTarget(GameObject target){
		Damage.doDamage(target, Damage.DamageEffect.None, 1);
	}
}
