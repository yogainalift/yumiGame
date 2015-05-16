using UnityEngine;

using System.Collections;

public class ArrowControl : MonoBehaviour {

	public bool isPickup = false;
	public int gravityRotate = 1;
	private Rigidbody myRigidBody;




	void Start(){
		this.myRigidBody = this.GetComponent<Rigidbody>();
	}

	void Update ()
	{
		//transform.up = -myRigidBody.velocity ;
		if (!myRigidBody.Equals(null)){
			transform.right =  myRigidBody.velocity;
		}
	}

	void OnCollisionEnter(Collision collision){

		//this.transform.parent = collision.gameObject.transform;
		Destroy (GetComponent<Rigidbody>());

		//creating empty object that has scale (1,1,1) to assign my object to remain unscaled
		GameObject unScaledParent = new GameObject();
		DestroyObject(unScaledParent, 5);
		unScaledParent.transform.parent = collision.transform;
		transform.parent = unScaledParent.transform;

	}


}