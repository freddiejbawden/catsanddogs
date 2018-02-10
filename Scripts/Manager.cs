using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    private float gameTime = 1.0f;
    public float timeModifier;

    public static Manager instance;

    void Awake() {
        if(instance != null) {
            Destroy(instance);
        }

        instance = this;
    }

    void Update() {
        timeModifier = Time.deltaTime * gameTime;
    }

    public void changeGameTime(float gameTime) {
        this.gameTime = gameTime;
    }
}
