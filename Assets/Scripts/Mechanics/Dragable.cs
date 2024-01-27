using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dragable : MonoBehaviour
{
    Vector3 mousePosition;
    public bool active = true;

    public void deactivate() {
        active = false;
    }

    private Vector3 GetMousePos() {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown() {
        if (active) {
            mousePosition = Input.mousePosition - GetMousePos();
        }
    }

    private void OnMouseDrag() {
        if (active) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }
    }
}
