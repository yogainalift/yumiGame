﻿using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Transform target;
	private float trackSpeed = 10;

	public void SetTarget(Transform t){
		target = t;
	}

	void LateUpdate(){
		if (target){
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, target.position.y+3, trackSpeed);
			transform.position = new Vector3(x, y, transform.position.z);
		}
		if (target==null){
			Debug.Log("you dead somehow, reset target of cam");
			this.SetTarget(GameObject.FindWithTag("Player").transform);
		}
	}

	private float IncrementTowards(float n, float target, float a){
		if (n == target){
			return n;
		}
		else{
			float dir = Mathf.Sign(target - n);
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target - n))? n: target;
		}
	}
}
