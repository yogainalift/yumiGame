using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public float health;
	public GameObject ragdoll;

	public void TakeDamage(float dmg){
		health -=dmg;

		if (health<=0){
			Die();
		}
	}

	public void Die(){
		Debug.Log ("DIE");
		Ragdoll r =	(Instantiate(ragdoll, transform.position, transform.rotation) as GameObject).GetComponent<Ragdoll>();
		r.CopyPose(transform);
		Destroy(this.gameObject);
	}
}
