using System;
using System.Collections;
using UnityEngine;

public class OraOra : MonoBehaviour
{
    [SerializeField] Transform enemy;
    [SerializeField] float speed = 5f;
    [SerializeField] float intensity = 0.1f;
    [SerializeField] float punchDuration = 0.2f;
    [SerializeField] int goal = 6;
    [SerializeField] int timeLimit = 8;
    bool isBeingPunched = false;
    bool rotation = false;
    Vector3 startingPosition;
    Vector3 launchPosition = new Vector3(3, 3, 0);
    Vector3 launchScale = new Vector3(0.6f, 0.6f, 0);

    void Keypress (char c) {
        if (!Char.IsWhiteSpace(c)) return;
        if (goal > 0) {
            goal--;
            StartCoroutine(punch());
        } else {
            rotation = true;
            StartCoroutine(1.5f.Tweeng((p)=>enemy.position=p, enemy.position, launchPosition));
            StartCoroutine(1.5f.Tweeng((s)=>enemy.localScale=s, enemy.localScale, launchScale));
            StartCoroutine(Rotate());
            WinStage();
        }
    }

    IEnumerator Rotate() {
        rotation = true;
        yield return new WaitForSeconds(1.5f);
        rotation = false;
    }

    void Start () {
        startingPosition = enemy.position;
        GameManager.instance.StartStage(timeLimit);
    }

    // Update is called once per frame
    void Update () {
        if (isBeingPunched) {
            enemy.position = startingPosition + intensity * new Vector3 (
                Mathf.PerlinNoise(speed * Time.time, 1),
                Mathf.PerlinNoise(speed * Time.time, 2),
                0
            );
        }
        if (rotation) {
            enemy.Rotate(0, 0, 1);
        }
    }

    IEnumerator punch() {
        isBeingPunched = true;
        yield return new WaitForSeconds(punchDuration);
        isBeingPunched = false;
    }

    private void WinStage() {
        GameManager.instance.WinStage();
    }
}