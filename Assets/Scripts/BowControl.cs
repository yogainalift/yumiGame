using UnityEngine;
using System.Collections;

public class BowControl : MonoBehaviour {


	private bool isPressed;
	private Vector3 mousePos;


	void Start(){
		isPressed=false;
	}

	void Update() {

		if (Input.GetMouseButtonDown(0)){
			mousePos = Input.mousePosition;


			Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
			isPressed=true;

		}

		if (isPressed){
			Vector3 dir = mousePos-Input.mousePosition;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
			//Debug.DrawRay(transform.position, dir);



			/*dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);*/

		}

		if (Input.GetMouseButtonUp(0)){
			isPressed=false;
		}

	}

}