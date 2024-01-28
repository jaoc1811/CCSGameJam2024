using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Clickable : MonoBehaviour
{
    public bool active = true;

    public void deactivate() {
        active = false;
    }

    private void OnMouseDown() {
        if (active) {
            BroadcastMessage("Click");
        }
    }
}
