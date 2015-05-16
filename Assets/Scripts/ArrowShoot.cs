using UnityEngine;
using System.Collections;

public class ArrowShoot : MonoBehaviour {

	private Vector3 mousePos;
	private Vector3 offset;

	private LineRenderer lineRenderer;
	
	//arrow properties
	public Rigidbody arrowPrefab; 

	public float arrowSpeed = 100f;
	public float fireRate = 0.0f;
	public float nextFire = 0.0f;


	private bool falsePull;
	private bool isPulled;

	public float pullStartTime = 0.0f;
	public float pullTime = 0f;

	//public float maxStrengthPullTime = 1f; // how long to hold button until max strength reached

	void Start(){
		falsePull = false;
		isPulled = false;

		lineRenderer=GetComponent<LineRenderer>();

		lineRenderer.SetWidth (0.3f, 0.3f);
		lineRenderer.enabled=false;
	}
	
	void Update(){

		// pull back string
		if(Input.GetMouseButtonDown(0))
		{
			mousePos = Input.mousePosition;
			isPulled=true;
			lineRenderer.enabled=true;

			if(Time.time > nextFire)
			{
				nextFire = Time.time + fireRate; 
				pullStartTime = Time.time; //store the start time
				//animation.Play("PULLBACK");
			}
			else{
				falsePull = true;
			}
		}
		// fire arrow
		if(Input.GetMouseButtonUp(0)){
			if(!falsePull)
			{
				isPulled=false;
				lineRenderer.enabled=false;
				offset = mousePos - Input.mousePosition;

				/*
				//nextFire = Time.time + pullTime; // this is the actual fire rate as things stand now
				//animation.Play("FIRE");

				float timePulledBack = Time.time - pullStartTime; // this is how long the button was held
				if(timePulledBack > maxStrengthPullTime) // this says max strength is reached 
					timePulledBack = maxStrengthPullTime; // max strength is ArrowSpeed * maxStrengthPullTime
				arrowSpeed = arrowSpeed * timePulledBack; // adjust speed directly using pullback 
				*/
				if ( offset.magnitude > Vector3.Magnitude(new Vector3(2,2,0) )){ //only if user pulls hard enough
					
					Rigidbody arrowInstance = Instantiate(arrowPrefab, 			//the object
					                                      transform.position,	//the 3d pos
					                                      transform.rotation) 	//the rotation
						as Rigidbody;		//reference conversions to rigidbody
					//arrowPrefab = Rigidbody.Instantiate(arrowPrefab); //can also instantiate like this.
					arrowInstance.name = "Arrow";

					/*
					float timePulledBack = Time.time - pullStartTime; // this is how long the button was held

					if(timePulledBack > 0.4f){ // this says max strength is reached 
						arrowSpeed *= 1.5f; // adjust speed directly using pullback
					}
					if(timePulledBack>0.8f){
						arrowSpeed *= 3f; // adjust speed directly using pullback
					}
					*/

					if (offset.magnitude <= 100){
						arrowInstance.AddForce(offset * 10);
					}
					else{
						arrowInstance.AddForce(offset.normalized*1000);
					}
						
					DestroyObject(arrowInstance.gameObject, 5);
					arrowSpeed=300;
				}
			}
			else{
				falsePull = false;
			}
		}

		if (isPulled){
			offset= mousePos - Input.mousePosition;

			if (offset.magnitude <= 100){
				plotTrajectory(transform.position, offset*10 / 25f, 0.01f, 0.1f);
			}
			else{
				plotTrajectory(transform.position, offset.normalized*1000/25f, 0.01f, 0.1f);
			}

		}
	}

	public Vector3 plotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
		return start + startVelocity*time + Physics.gravity*time*time*0.5f;
	}
	
	public void plotTrajectory (Vector3 start, Vector3 startVelocity, float timestep, float maxTime) {
		//init
		Vector3 prev = start;
		int vertCount = 0;
		lineRenderer.SetVertexCount(vertCount);
		int lrIndex=0;
		//go
		for (int i=1;;i++) {
			float t = timestep*i;
			if (t > maxTime) break;
			Vector3 pos = plotTrajectoryAtTime(start, startVelocity, t);
			if (Physics.Linecast(prev,pos)){
				lineRenderer.material.mainTextureScale = new Vector2(20,1);
				if (vertCount>=1)
					lineRenderer.SetVertexCount(vertCount-1);
				break;
			}
			//lineRenderer.material.
			if (offset.magnitude>=100){
				lineRenderer.material.mainTextureScale = new Vector2(10,1);
			} else{
				lineRenderer.material.mainTextureScale = new Vector2(offset.magnitude/10,1);
			}

			Debug.DrawLine(prev,pos);
			vertCount+=1;
			lineRenderer.SetVertexCount(vertCount);
			lineRenderer.SetPosition(lrIndex,prev);
			lrIndex+=1;
			prev = pos;
		}
	}
}