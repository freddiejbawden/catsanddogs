using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {
	public float speed;
	public Vector2 target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, target, speed*Time.deltaTime);
	}
}
