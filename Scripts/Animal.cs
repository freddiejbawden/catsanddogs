using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
	public bool isDog;
	private float fallspeed;
	// Use this for initialization
	void Start () {
		if (isDog) {
			initDog();
		} else {
			initCat();
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * Manager.instance.timeModifier * 180);

		Vector2 pos = new Vector2(transform.position.x, transform.position.y - fallspeed * Manager.instance.timeModifier);
		transform.position = pos;
	}

	void initDog () {
		fallspeed = 30.0f;
	}

	void initCat () {
		fallspeed = 150.0f;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Contains("Ground")) {
			Destroy(gameObject);
		}
	}

}
