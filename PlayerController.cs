using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public float jumpVelocity = 10;
	public LayerMask playerMask;
	Transform myTrans, tagGround;
	Rigidbody myBody;

	bool isGrounded = false;

	void Start () {
		myBody = this.GetComponent<Rigidbody>();
		myTrans = this.transform;
		tagGround = GameObject.Find (this.name+"/tag_ground").transform;
	}
	

	void FixedUpdate () {
		isGrounded = Physics.Linecast(myTrans.position, tagGround.position, playerMask);

		Move(Input.GetAxisRaw("Horizontal"));
		if (Input.GetButtonDown("Jump")){
			Jump();
		}
	}

	public void Move(float horizontalInput){
		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizontalInput * speed;
		myBody.velocity = moveVel;
	}

	public void Jump(){
		if(isGrounded){
			myBody.velocity += jumpVelocity * Vector3.up;
		}
	}
}