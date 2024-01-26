using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Clickable : MonoBehaviour
{
    private void OnMouseDown() {
        BroadcastMessage("Click");
    }
}
