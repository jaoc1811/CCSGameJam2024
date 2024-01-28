using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] int lifeNumber;

    // Start is called before the first frame update
    void Start()
    {
        if (lifeNumber >= GameManager.instance.lives) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
