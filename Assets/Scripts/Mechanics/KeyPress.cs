using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPress : MonoBehaviour
{
    public bool active = true;

    public void deactivate() {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
        foreach (char c in Input.inputString)
        {
            BroadcastMessage("Keypress", c);
        }
    }
}
