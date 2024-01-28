using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clown : MonoBehaviour
{
    [SerializeField] Animator clownAnimator;
    [SerializeField] AudioClip sadSong;
    [SerializeField] GameObject canvas;
    bool winStage;

    // Start is called before the first frame update
    void Start()
    {
        clownAnimator = GameObject.Find("Clown").GetComponent<Animator>();
        winStage = GameManager.instance.StageCleared;

        if (!GameManager.instance.GameStarting) {
            if (GameManager.instance.lives == 1 && GameManager.instance.StageCleared == false) {
                StartCoroutine(sadAnimation());
            } else {
                if (winStage) {
                    StartCoroutine(happyAnimation());
                } else {
                    StartCoroutine(dissapointedAnimation());
                }
            }
           
        }
    }

    public void ReloadGame() {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Hub");
    }

    IEnumerator happyAnimation() {
        clownAnimator.SetBool("happy", true);
        Camera.main.orthographicSize = 2;
        Camera.main.transform.position = new Vector3(0, -1.3f, -10);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(.5f.Tweeng((s) => Camera.main.orthographicSize = s, 2, 5));
        StartCoroutine(.5f.Tweeng((p) => Camera.main.transform.position = p, new Vector3(0, -1.3f, -10), new Vector3(0, 0, -10)));
        yield return new WaitForSeconds(1f);
        clownAnimator.SetBool("happy", false);
    }
    
    IEnumerator dissapointedAnimation() {
        clownAnimator.SetBool("dissapointed", true);
        Camera.main.orthographicSize = 2;
        Camera.main.transform.position = new Vector3(0, -1.3f, -10);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(.5f.Tweeng((s) => Camera.main.orthographicSize = s, 2, 5));
        StartCoroutine(.5f.Tweeng((p) => Camera.main.transform.position = p, new Vector3(0, -1.3f, -10), new Vector3(0, 0, -10)));
        yield return new WaitForSeconds(1f);
        clownAnimator.SetBool("dissapointed", false);
    }

    IEnumerator sadAnimation() {
        GetComponent<AudioSource>().clip = sadSong;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        clownAnimator.SetBool("sad", true);
        StartCoroutine(10f.Tweeng((s) => Camera.main.orthographicSize = s, 5, 2));
        StartCoroutine(10f.Tweeng((p) => Camera.main.transform.position = p, new Vector3(0, 0, -10), new Vector3(0, -1.3f, -10)));
        yield return new WaitForSeconds(3f);
        canvas.SetActive(true);
    }
}
