using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Pedrito : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] float speed = 5f;
    [SerializeField] float intensity = 0.1f;
    [SerializeField] float electrocuteDuration = 0.2f;
    [SerializeField] int goal = 6;
    [SerializeField] int timeLimit = 8;

    bool isElectrocuting = false;

    Vector3 startingPosition;

    void Click () {
        if (goal > 0) {
            goal--;
            StartCoroutine(electrocute());
        } else {
            WinStage();
        }

    }

    void Start () {
        startingPosition = npc.transform.position;
        GameManager.instance.StartStage(timeLimit);
    }
    // Update is called once per frame
    void Update () {
        if (isElectrocuting) {
            npc.transform.position = startingPosition + intensity * new Vector3 (
                Mathf.PerlinNoise(speed * Time.time, 1),
                Mathf.PerlinNoise(speed * Time.time, 2),
                0
            );
        }
    }

    IEnumerator electrocute() {
        isElectrocuting = true;
        yield return new WaitForSeconds(electrocuteDuration);
        isElectrocuting = false;
    }

    private void WinStage() {
        isElectrocuting = true;
        GameManager.instance.WinStage();
    }
}
