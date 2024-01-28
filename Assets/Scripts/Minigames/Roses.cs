using UnityEngine;
using System;
using System.Collections;

public class Roses : MonoBehaviour
{
    [SerializeField] int targetRoses = 5;
    [SerializeField] int currentRoses = 0;
    [SerializeField] int timeLimit = 8;
    [SerializeField] Sprite BlinkSprite;
    [SerializeField] Sprite OpenEyesSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AudioClip unaRosa;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameManager.instance.StartStage(Color.black, timeLimit);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentRoses += 1;
        other.gameObject.GetComponent<Dragable>().deactivate();
        StartCoroutine(ReceiveRose());
        if (currentRoses >= targetRoses) {
            StopAllCoroutines();
            WinStage();
        }
    }

    IEnumerator ReceiveRose() {
        Blink();
        AudioSource.PlayClipAtPoint(unaRosa, Camera.main.transform.position, 0.15f);
        yield return new WaitForSeconds(.2f);
        OpenEyes();
    }

    void Blink(){
        spriteRenderer.sprite = BlinkSprite;
    }

    void OpenEyes(){
        spriteRenderer.sprite = OpenEyesSprite;
    }

    private void WinStage() {
        // TODO: Trigger success animation/sounds
        StartCoroutine(WinBlinks());
        GameManager.instance.WinStage();
    }

    IEnumerator WinBlinks() {
        while (true){
            Blink();
            yield return new WaitForSeconds(.5f);
            OpenEyes();
            yield return new WaitForSeconds(.5f);
        }
    }
}
