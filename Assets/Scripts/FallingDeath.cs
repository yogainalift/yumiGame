using UnityEngine;
using System.Collections;

public class FallingDeath : MonoBehaviour {

	//GameManager gm = new GameManager();
	public Transform spawnPoint;
	public GameObject player;

	void OnTriggerEnter(Collider c) {
		Debug.Log("heyyo");
		if (c.tag == "Player"){
			Destroy (GameObject.FindWithTag("Player"));
			GameObject playerInstance= Instantiate(player, spawnPoint.transform.position, Quaternion.identity) as GameObject;
			playerInstance.name="Player";

		}
	}
//	
//	// Update is called once per frame
//	void LateUpdate () {
//
//		if (transform.position.y < -5){
//			GameObject playerInstance= Instantiate(player, spawnPoint.transform.position, Quaternion.identity) as GameObject;
//			playerInstance.name="Player";
//			Destroy (this.gameObject);
//			//Debug.Log(" you dead ");
//		}
//	}

}