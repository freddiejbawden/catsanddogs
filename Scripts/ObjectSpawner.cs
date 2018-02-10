using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour {
	public float spawnTime;
	float time = 0;
	public GameObject[] animals = new GameObject[2];

	// Use this for initialization
	void Start () {
		spawnTime = spawnTimer();
	}

	// Update is called once per frame
	void Update () {
		time += Manager.instance.timeModifier;
		spawner();
	}

	void spawner () {
		if (time >= spawnTime) {
			if (Random.Range(0,2) % 2 == 0) {
				spawnEntity(animals[0]);
			} else {
				spawnEntity(animals[1]);
			}
			time = 0;
			spawnTime = spawnTimer() / 5.0f;
		}
	}

	void spawnEntity (GameObject animal) {
		Vector2 pos = new Vector2(transform.position.x + spawnPos(), transform.position.y);
		Instantiate(animal, pos, Quaternion.identity);
	}

	float spawnPos() {
		float randomFloat = Random.Range(-50,50);
		return randomFloat;
	}

	float spawnTimer() {
		int randomInt = Random.Range(1,10);
		return (float)randomInt;
	}

}
