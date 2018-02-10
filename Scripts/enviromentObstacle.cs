using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enviromentObstacle : MonoBehaviour {
	public int numOfObjs;
	public GameObject e1;
	public GameObject[] obs = new GameObject[1];
	private float time = 0;
	void Start() {
		//fix this shite bag code
		obs[0] = e1;
	}
	// Use this for initialization
	void SpawnObs() {
		Instantiate (chooseGameObject (),transform);
	}
	GameObject chooseGameObject() {
		int randIndex = Random.Range (0, obs.Length);
		return obs[randIndex];
	}
	void Update() {
		if (time >= 3f) {
			SpawnObs ();
			time = Random.Range (0f, 5f);
		} else {
			time += Time.deltaTime;
		}
	}
}
