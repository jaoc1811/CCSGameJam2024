using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            BroadcastMessage("Keypress", c);
        }
    }
}
