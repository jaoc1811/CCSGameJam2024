using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tickles : MonoBehaviour
{

    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotation;
    [SerializeField] Transform feather;
    [SerializeField] Transform otherFeet;
    bool inFeet;
    bool isMoving;
    public bool tickles;
    Vector3 lastPosition;
    int tickleCount;
    // Start is called before the first frame update
    void Start()
    {
        inFeet = false;
        lastPosition = feather.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFeet) {
            if ( feather.position != lastPosition )
                isMoving = true;
            else
                isMoving = false;
            lastPosition = feather.position;
        }
    }

    void FixedUpdate() {
        if (isMoving) {
            tickles = true;
            tickleCount++;
            if (tickleCount > 100) {
                otherFeet.GetComponent<Tickles>().isMoving = true;
            }
        } else {
            tickles = false;
        }

        if (tickles) {
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
}
