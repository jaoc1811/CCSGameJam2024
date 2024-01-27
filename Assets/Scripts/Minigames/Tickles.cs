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
    bool inFeet;
    bool isMoving;
    bool tickles;
    public bool winTickles;
    Vector3 lastPosition;
    int tickleCount;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartStage(timeLimit);
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
            tickles = true;
            tickleCount++;
            if (tickleCount > 50) {
                WinStage();
            }
        } else {
            tickles = false;
        }

        if (tickles || winTickles) {
            float buoyancyRotation = Mathf.PingPong(Time.time * rotationSpeed, maxRotation) - (maxRotation/2);
            transform.eulerAngles = new Vector3(transform.eulerAngles.y,transform.eulerAngles.y,buoyancyRotation);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        inFeet = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        inFeet = false;
    }

    private void WinStage() {
        otherFeet.GetComponent<Tickles>().winTickles = true;
        winTickles = true;
        feather.GetComponent<Dragable>().deactivate();
        GameManager.instance.WinStage();
    }
}
