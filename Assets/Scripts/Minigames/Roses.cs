using UnityEngine;
using System;
using System.Collections;

public class Roses : MonoBehaviour
{
    [SerializeField] int targetRoses = 5;
    [SerializeField] int currentRoses = 0;
    [SerializeField] Sprite BlinkSprite;
    [SerializeField] Sprite OpenEyesSprite;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
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
        yield return new WaitForSeconds(.2f);
        OpenEyes();
    }

    void Blink(){
        spriteRenderer.sprite = BlinkSprite;
        Debug.Log("blink");
    }

    void OpenEyes(){
        spriteRenderer.sprite = OpenEyesSprite;
        Debug.Log("open eyes");
    }

    private void WinStage() {
        // TODO: Trigger success animation/sounds
        StartCoroutine(WinBlinks());
        // GameManager.instance.WinStage();
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
