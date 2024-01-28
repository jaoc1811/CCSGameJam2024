using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dragable : MonoBehaviour
{
    Vector3 mousePosition;
    public bool active = true;
    [SerializeField] AudioClip mouseDown;
    [SerializeField] AudioClip mouseUp;
    bool click;

    public void deactivate() {
        active = false;
    }

    private Vector3 GetMousePos() {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown() {
        if (active) {
            if (!click){
                AudioSource.PlayClipAtPoint(mouseDown, Camera.main.transform.position, 0.3f);
                click = true;
            }
            mousePosition = Input.mousePosition - GetMousePos();
        }
    }

    private void OnMouseUp() {
        if (active) {
            if (click){
                AudioSource.PlayClipAtPoint(mouseUp, Camera.main.transform.position, 0.3f);
                click = false;
            }
        }
    }

    private void OnMouseDrag() {
        if (active) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }
    }
}
