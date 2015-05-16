using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject player;
	private GameCamera cam;


	void Start () {
		cam = GetComponent<GameCamera>();
		SpawnPlayer();
	}
	
	public void SpawnPlayer(){
		GameObject playerInstance = (Instantiate(player, spawnPoint.transform.position, Quaternion.identity)) as GameObject;
		playerInstance.name="Player";
		cam.SetTarget(playerInstance.transform);
		
	}
}
