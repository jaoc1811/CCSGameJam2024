using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("Se hizo clic en el objeto: " + hit.transform.name);
            }
        }
    }
  
}
