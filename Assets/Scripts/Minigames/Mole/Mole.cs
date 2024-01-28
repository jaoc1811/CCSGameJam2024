using System;
using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField] Transform[] holes;
    [SerializeField] float distanceToMove = 1;
    [SerializeField] float timeToMove = 0.5f;
    [SerializeField] float timeHiding = 1f;
    [SerializeField] float garbageChance = 0.1f;
    [SerializeField] Transform garbage;
    [SerializeField] Sprite successSprite;
    [SerializeField] Animator failAnimation;
    Transform currentMole;
    public bool clickable = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartStage();
        ComeOut();
    }

    [ContextMenu("In")]
    void Hide() {
        Vector3 startPosition = currentMole.transform.position;
        Vector3 finalPosition = currentMole.transform.position - new Vector3(0, distanceToMove, 0);
        StartCoroutine(timeToMove.Tweeng((p) => currentMole.transform.position = p, startPosition, finalPosition));
        StartCoroutine(WaitAndRun(ComeOut, timeHiding));
    }

    [ContextMenu("Out")]
    void ComeOut() {
        ChooseMole();
        currentMole.transform.position = holes[UnityEngine.Random.Range(0, holes.Length)].transform.position;
        Vector3 startPosition = currentMole.transform.position;
        Vector3 finalPosition = currentMole.transform.position + new Vector3(0, distanceToMove, 0);
        StartCoroutine(timeToMove.Tweeng((p) => currentMole.transform.position = p, startPosition, finalPosition));
        StartCoroutine(WaitAndRun(Hide, 0f));
    }

    void ChooseMole() {
        float chance = UnityEngine.Random.Range(0f, 1f);
        if (chance < garbageChance) {
            currentMole = garbage;
        }
        else {
            currentMole = transform;
        }
    }

    void Click() {
        if (clickable) {
            StopAllCoroutines();
            WinStage();
        }
    }

    public void LoseStage() {
        // TODO: Trigger fail animation/sounds
        StopAllCoroutines();
        failAnimation.enabled = true;
    }

    void WinStage() {
        // TODO: Trigger success animation/sounds
        StopAllCoroutines();
        gameObject.GetComponent<SpriteRenderer>().sprite = successSprite;
        gameObject.transform.localScale = new Vector3(1,1,1);
        GameManager.instance.WinStage();
    }

    IEnumerator WaitAndRun(Action action, float extraTime) {
        yield return new WaitForSeconds(timeToMove + extraTime);
        action();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        clickable = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        clickable = false;
    }
}
