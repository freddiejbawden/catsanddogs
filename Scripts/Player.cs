using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private bool canJump;
    private bool canKick;
    private bool isStunned;
    [SerializeField] private bool wasd;
    private int faceDir;
    private float jumpSpeed = 500.0f;
    private float kickDelay = 0.0f;
    private float kickSpeed = 500.0f;
    private float speed = 5.0f;
    private GameObject otherPlayer;
    private int xMove = 0;
    private int yMove = 0;
    [SerializeField] private Sprite[] sprites = new Sprite[2];
    private SpriteRenderer rend;

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

    void Start() {
        rend = gameObject.GetComponent<SpriteRenderer>();
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
                    faceDir = -1;
                }

                if(Input.GetKey(KeyCode.D)) {
                    xMove++;
                    faceDir = 1;
                }

                if(Input.GetKey(KeyCode.W) && canJump) {
                    yMove = 1;
                }

                if(Input.GetKeyDown(KeyCode.LeftShift)) {
                    if(kickDelay <= 0) {
                        kick();
                    }
                }
            } else {
                if(Input.GetKey(KeyCode.J)) {
                    xMove--;
                    faceDir = -1;
                }

                if(Input.GetKey(KeyCode.L)) {
                    xMove++;
                    faceDir = 1;
                }

                if(Input.GetKey(KeyCode.I) && canJump) {
                    yMove = 1;
                }

                if(Input.GetKeyDown(KeyCode.RightControl)) {
                    if(kickDelay <= 0) {
                        kick();
                    }
                }
            }
						Debug.Log(faceDir);
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
        if(canKick) {
            Vector2 kickForce = new Vector2(kickSpeed * faceDir, 0.0f);
            otherPlayer.GetComponent<Rigidbody2D>().AddForce(kickForce);
        }
        rend.sprite = sprites[1];
        kickDelay = 0.4f;
    }

    private void move() {
        if(kickDelay > 0) {
            kickDelay -= Manager.instance.timeModifier;
        } else {
            kickDelay = 0;
            rend.sprite = sprites[0];
        }
				if (faceDir == 1) {
					transform.rotation = Quaternion.Euler(0,0,0);
				} else {
					transform.rotation = Quaternion.Euler(0,-180.0f,0);

				}
        Vector3 move = new Vector3(xMove, yMove, -1) * speed * Manager.instance.timeModifier;
        transform.position = new Vector3(transform.position.x + move.x, transform.position.y, -1);

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
