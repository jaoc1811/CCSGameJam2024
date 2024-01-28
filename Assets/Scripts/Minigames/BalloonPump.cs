using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPump : MonoBehaviour
{
    [SerializeField] float maxY = -0.3f;
    [SerializeField] float minY = -1.4f;
    [SerializeField] int timeLimit = 8;
    [SerializeField] Transform balloon;
    [SerializeField] Transform clown;
    [SerializeField] float balloonInflate;
    [SerializeField] float balloonMaxScale;
    [SerializeField] AudioClip pumpSound;
    [SerializeField] AudioClip wooh;
    Vector3 initialPosition;
    Vector3 screenPosition;
    bool active = true;
    bool pumpable = true;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        GameManager.instance.StartStage(Color.black, timeLimit);
    }

    public void deactivate() {
        active = false;
    }

    private Vector3 GetPos() {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown() {
        if (active) {
            screenPosition = Input.mousePosition - GetPos();
        }
    }

    private void OnMouseDrag() {
        if (active) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - screenPosition);
            float y = Mathf.Clamp(transform.position.y, minY, maxY);
            transform.position = new Vector3(initialPosition.x, y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Up") {
            pumpable = true;
        }
        else if (other.name == "Down" && pumpable==true) {
            pumpable = false;
            AudioSource.PlayClipAtPoint(pumpSound, Camera.main.transform.position, 0.3f);
            balloon.localScale += Vector3.one*balloonInflate;
            if (balloon.localScale.x >= balloonMaxScale) {
                WinStage();
                active = false;
            }
        }
    }

    void WinStage() {
        AudioSource.PlayClipAtPoint(wooh, Camera.main.transform.position, 0.1f);
        clown.GetComponent<Animator>().enabled = true;
        GameManager.instance.WinStage();
    }
}
