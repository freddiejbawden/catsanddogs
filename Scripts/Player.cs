using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private bool canJump;
    private bool canKick;
    private bool isStunned;
    [SerializeField] private bool wasd;
    private float jumpSpeed = 500.0f;
    private float kickDelay = 0.0f;
    private float kickSpeed = 500.0f;
    private float speed = 5.0f;
    private GameObject otherPlayer;
    private int xMove = 0;
    private int yMove = 0;

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag.Contains("Jump")) {
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag.Contains("Death")) {
            die();
        }

        if(col.gameObject.tag.Contains("Player")) {
            canKick = true;
            otherPlayer = col.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.tag.Contains("Death")) {

        }

        if(col.gameObject.tag.Contains("Jump")) {
            canJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.tag.Contains("Player")) {
            canKick = false;
        }
    }

    void Update () {
        input();
        move();
    }

    private void die() {
        Destroy(gameObject);
    }

    private void input() {
        xMove = 0;
        yMove = 0;

        if(!isStunned) {
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

                if(Input.GetKey(KeyCode.Space) && canKick) {
                    if(kickDelay < 0) {
                        kick();
                    }
                }
            } else {
                if(Input.GetKey(KeyCode.Keypad4)) {
                    xMove--;
                }

                if(Input.GetKey(KeyCode.Keypad6)) {
                    xMove++;
                }

                if(Input.GetKey(KeyCode.Keypad8) && canJump) {
                    yMove = 1;
                } else if(Input.GetKey(KeyCode.Keypad5)) {
                    yMove = -1;
                }

                if(Input.GetKey(KeyCode.RightShift) && canKick) {
                    if(kickDelay < 0) {
                        kick();
                    }
                }
            }
        } else {
            StartCoroutine(stopStun());
        }
    }

    private void jump() {
        Vector2 jumpForce = new Vector2(0.0f, jumpSpeed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpForce);
        canJump = false;
    }

    private void kick() {
        Vector2 kickForce = new Vector2(kickSpeed * xMove, 0.0f);
        otherPlayer.GetComponent<Rigidbody2D>().AddForce(kickForce);
        kickDelay = 0.2f;
    }

    private void move() {
        kickDelay -= Manager.instance.timeModifier;

        Vector2 move = new Vector2(xMove, yMove) * speed * Manager.instance.timeModifier;
        transform.position = new Vector2(transform.position.x + move.x, transform.position.y);

        if(yMove == 1) {
            jump();
        }
    }

    private IEnumerator stopStun() {
        yield return new WaitForSeconds(1);

        isStunned = false;
        StopAllCoroutines();
    }

    public void stun() {
        isStunned = true;
    }
}
