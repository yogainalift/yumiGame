using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class Controller : MonoBehaviour{ 

	public LayerMask collisionMask;


	const float skinWidth= .015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	private BoxCollider collider;
	RaycastOrigins raycastOrigins;
	RaycastHit hit;
	public CollisionInfo collisions;

	void Start(){
		collider = GetComponent<BoxCollider>();
		CalculateRaySpacing();
	}
	
	public void Move(Vector3 velocity){
		UpdateRaycastOrigins();
		collisions.Reset();

		if (velocity.x !=0) {
			HorizontalCollisions(ref velocity);
		}
		if (velocity.y !=0){
			VerticalCollisions(ref velocity);
		}
		transform.Translate (velocity);
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector3 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector3.up * (horizontalRaySpacing * i);
			Ray ray = new Ray(rayOrigin, Vector3.right * directionX * rayLength);
			
			Debug.DrawRay(rayOrigin, Vector3.right * directionX * rayLength, Color.red);
			
			if (Physics.Raycast(ray, out hit, rayLength, collisionMask)){
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity){
		float directionY=Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for (int i=0 ; i< verticalRayCount ; i++){
			Vector3 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector3.right * (verticalRaySpacing * i+ velocity.x);
			Ray ray = new Ray(rayOrigin, Vector3.up * directionY);

			Debug.DrawRay(rayOrigin, Vector3.up *directionY * rayLength, Color.red ); 

			if (Physics.Raycast(ray, out hit, rayLength, collisionMask)){
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}
	}
	
	void UpdateRaycastOrigins(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth *-2);
		
		raycastOrigins.bottomLeft = new Vector3 (bounds.min.x, bounds.min.y, 0);
		raycastOrigins.bottomRight = new Vector3 (bounds.max.x, bounds.min.y, 0);
		raycastOrigins.topLeft = new Vector3 (bounds.min.x, bounds.max.y, 0);
		raycastOrigins.topRight = new Vector3 (bounds.max.x, bounds.max.y, 0);
	}

	void CalculateRaySpacing(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth *-2);
		
		horizontalRayCount= Mathf.Clamp(horizontalRayCount, 2,int.MaxValue);
		verticalRayCount= Mathf.Clamp(verticalRayCount, 2,int.MaxValue);
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins{
		public Vector3 topLeft, topRight;
		public Vector3 bottomLeft, bottomRight;
		
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
		
		public void Reset() {
			above = below = false;
			left = right = false;
		}
	}

	
}