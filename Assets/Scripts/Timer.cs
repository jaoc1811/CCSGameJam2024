using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Time >= 0){
            gameObject.GetComponent<TMP_Text>().text = GameManager.instance.Time.ToString();
        }
    }
}
