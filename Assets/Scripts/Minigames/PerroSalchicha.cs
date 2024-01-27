using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroSalchicha : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotation;
    [SerializeField] GameObject hand;
    [SerializeField] int timeLimit = 8;
    [SerializeField] float pettingLimit = 50;
    bool inDog;
    bool isMoving;
    bool petting;
    bool winPetting;
    Vector3 lastPosition;
    int pettingCount;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartStage(timeLimit);
        inDog = false;
        lastPosition = hand.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inDog) {
            if ( hand.transform.position != lastPosition )
                isMoving = true;
            else {
                isMoving = false;
            }
        } else {
            isMoving = false;
        }
        lastPosition = hand.transform.position;
        if (isMoving) {
            petting = true;
            pettingCount++;
            if (pettingCount > pettingLimit) {
                WinStage();
            }
        } else {
            petting = false;
        }

        if (petting || winPetting) {
            float rotation = Mathf.PingPong(Time.time * rotationSpeed, maxRotation) - (maxRotation/2);
            transform.GetChild(0).eulerAngles = new Vector3(transform.eulerAngles.y,transform.eulerAngles.y,rotation);
            transform.GetChild(1).eulerAngles = new Vector3(transform.eulerAngles.y,transform.eulerAngles.y,-rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        inDog = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        inDog = false;
    }

    private void WinStage() {
        winPetting = true;
        hand.GetComponent<Point>().deactivate();
        GameManager.instance.WinStage();
    }
}
