using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tickles : MonoBehaviour
{

    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotation;
    [SerializeField] GameObject feather;
    [SerializeField] Transform otherFeet;
    [SerializeField] int timeLimit = 8;
    [SerializeField] AudioClip tickleSound;
    [SerializeField] AudioClip laughSound;
    bool inFeet;
    bool isMoving;
    bool tickles;
    public bool winTickles;
    Vector3 lastPosition;
    int tickleCount;
    bool laugh;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Foot1") {
          GameManager.instance.StartStage(Color.black, timeLimit);
        }
        inFeet = false;
        lastPosition = feather.transform.position;
    }

    void FixedUpdate() {
        if (inFeet) {
            if ( feather.transform.position != lastPosition )
                isMoving = true;
            else {
                isMoving = false;
            }
        } else {
            isMoving = false;
        }
        lastPosition = feather.transform.position;
        if (isMoving) {
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().PlayOneShot(tickleSound);
            }
            tickles = true;
            tickleCount++;
            if (tickleCount > 50) {
                WinStage();
            }
        } else {
            tickles = false;
        }

        if (tickles || winTickles) {
            float rotation = Mathf.PingPong(Time.time * rotationSpeed, maxRotation) - (maxRotation/2);
            transform.eulerAngles = new Vector3(transform.eulerAngles.y,transform.eulerAngles.y,rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        inFeet = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        inFeet = false;
    }

    private void WinStage() {
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = laughSound;
        GetComponent<AudioSource>().Play();
        otherFeet.GetComponent<Tickles>().winTickles = true;
        winTickles = true;
        feather.GetComponent<Dragable>().deactivate();
        GameManager.instance.WinStage();
    }
}
