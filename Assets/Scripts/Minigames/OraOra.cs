using System;
using System.Collections;
using UnityEngine;

public class OraOra : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float speed = 5f;
    [SerializeField] float intensity = 0.1f;
    [SerializeField] float punchDuration = 0.2f;
    [SerializeField] int goal = 6;
    [SerializeField] int timeLimit = 8;
    [SerializeField] AudioClip oraOra;
    [SerializeField] AudioClip lastOra;
    [SerializeField] AudioClip wryyy;
    [SerializeField] AudioClip starPlatinum;
    [SerializeField] Sprite aliveDio;
    [SerializeField] Sprite deadDio;
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
            WinStage();
        }
    }

    IEnumerator Rotate() {
        rotation = true;
        yield return new WaitForSeconds(1.5f);
        rotation = false;
    }

    void Start () {
        startingPosition = enemy.transform.position;
        AudioSource.PlayClipAtPoint(starPlatinum, Camera.main.transform.position, 0.3f);  
        GameManager.instance.StartStage(timeLimit);
    }

    // Update is called once per frame
    void Update () {
        if (isBeingPunched) {
            enemy.transform.position = startingPosition + intensity * new Vector3 (
                Mathf.PerlinNoise(speed * Time.time, 1),
                Mathf.PerlinNoise(speed * Time.time, 2),
                0
            );
        } else {
            if (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "OraOra") {
                GetComponent<Animator>().SetBool("punching", false);
            }
        }
        if (rotation) {
            enemy.transform.Rotate(0, 0, 1);
        }
    }

    IEnumerator punch() {
        GetComponent<Animator>().SetBool("punching", true);
        AudioSource.PlayClipAtPoint(oraOra, Camera.main.transform.position, 0.6f);
        isBeingPunched = true;
        yield return new WaitForSeconds(punchDuration);
        isBeingPunched = false;
    }

    private void WinStage() {
        rotation = true;
        GetComponent<AudioSource>().Stop();
        GetComponent<KeyPress>().deactivate();
        AudioSource.PlayClipAtPoint(lastOra, Camera.main.transform.position, 0.3f);
        AudioSource.PlayClipAtPoint(wryyy, Camera.main.transform.position, 0.3f);
        StartCoroutine(1.5f.Tweeng((p)=>enemy.transform.position=p, enemy.transform.position, launchPosition));
        StartCoroutine(1.5f.Tweeng((s)=>enemy.transform.localScale=s, enemy.transform.localScale, launchScale));
        StartCoroutine(Rotate());
        enemy.GetComponent<SpriteRenderer>().sprite = deadDio;
        GameManager.instance.WinStage();
    }
}
