using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private bool canJump;
    [SerializeField] private bool wasd;
    private float jumpSpeed = 500.0f;
    private float speed = 5.0f;
    private int xMove = 0;
    private int yMove = 0;

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag.Contains("Jump")) {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.tag.Contains("Jump")) {
            canJump = false;
        }
    }

    void Update () {
        input();
        move();
	}

    private void input() {
        xMove = 0;
        yMove = 0;

        if(wasd) {
            if(Input.GetKey(KeyCode.A)) {
                xMove--;
            }

            if(Input.GetKey(KeyCode.D)) {
                xMove++;
            }

            if(Input.GetKey(KeyCode.W) && canJump) {
                yMove = 1;
            } else if(Input.GetKey(KeyCode.S)) {
                yMove = -1;
            }
        } else {
            if(Input.GetKey(KeyCode.LeftArrow)) {
                xMove--;
            }

            if(Input.GetKey(KeyCode.RightArrow)) {
                xMove++;
            }

            if(Input.GetKey(KeyCode.UpArrow) && canJump) {
                yMove = 1;
            } else if(Input.GetKey(KeyCode.DownArrow)) {
                yMove = -1;
            }
        }
    }

    private void jump() {
        Vector2 jumpForce = new Vector2(0.0f, jumpSpeed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        canJump = false;
    }

    private void move() {
        Vector2 move = new Vector2(xMove, yMove) * speed * Manager.instance.timeModifier;
        transform.position = new Vector2(transform.position.x + move.x, transform.position.y);

        if(yMove == 1) {
            jump();
        }
    }
}
