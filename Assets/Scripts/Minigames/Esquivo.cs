using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esquivo : MonoBehaviour
{
    [SerializeField] GameObject[] positionsAndScales;
    [SerializeField] GameObject pointer;
    bool pointing = false;
    int pointingCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartStage(8);
        int randomIndex = Random.Range(0, positionsAndScales.Length);
        transform.position = positionsAndScales[randomIndex].transform.position;
        transform.localScale = positionsAndScales[randomIndex].transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (pointing) {
            pointingCount++;
            if (pointingCount > 60) {
                WinStage();
            }
        } 
    }

    IEnumerator Turn() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Turn());
    }


    private void OnTriggerEnter2D(Collider2D other) {
        pointing = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        pointing = false;
    }

    private void WinStage() {
        pointer.GetComponent<Point>().deactivate();
        StartCoroutine(Turn());
        // GameManager.instance.WinStage();
        pointing = false;
    }
    
}
