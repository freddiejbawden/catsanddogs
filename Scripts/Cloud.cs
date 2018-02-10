using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour {
    public AudioClip thunder;
    private AudioSource source;
    private Coroutine flash;
    private int randTime;
    public Sprite[] sprites = new Sprite[2];
    private SpriteRenderer rend;

    void Start() {
        rend = gameObject.GetComponent<SpriteRenderer>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = thunder;
        randTime = Random.Range(4, 15);

        StartCoroutine(thunderRoutine());
    }

    private IEnumerator cloudFlash() {
        rend.sprite = sprites[1];

        yield return new WaitForSeconds(0.7f);

        rend.sprite = sprites[0];

        yield return new WaitForSeconds(0.5f);

        rend.sprite = sprites[1];

        yield return new WaitForSeconds(0.2f);

        rend.sprite = sprites[0];

        StopCoroutine("flash");
    }

    private IEnumerator thunderRoutine() {
        yield return new WaitForSeconds(randTime);

        flash = StartCoroutine(cloudFlash());

        source.PlayOneShot(thunder);
        randTime = Random.Range(4, 15);
    }
}