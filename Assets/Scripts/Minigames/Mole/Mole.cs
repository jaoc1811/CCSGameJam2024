using System;
using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField] Transform[] holes;
    [SerializeField] float distanceToMove = 1;
    [SerializeField] float timeToMove = 0.5f;
    [SerializeField] float garbageChance = 0.1f;
    [SerializeField] Transform garbage;
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
        StartCoroutine(WaitAndRun(ComeOut));
    }

    [ContextMenu("Out")]
    void ComeOut() {
        ChooseMole();
        currentMole.transform.position = holes[UnityEngine.Random.Range(0, holes.Length)].transform.position;
        Vector3 startPosition = currentMole.transform.position;
        Vector3 finalPosition = currentMole.transform.position + new Vector3(0, distanceToMove, 0);
        StartCoroutine(timeToMove.Tweeng((p) => currentMole.transform.position = p, startPosition, finalPosition));
        StartCoroutine(WaitAndRun(Hide));
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
    }

    void WinStage() {
        // TODO: Trigger success animation/sounds
        StopAllCoroutines();
        GameManager.instance.WinStage();
    }

    IEnumerator WaitAndRun(Action action) {
        yield return new WaitForSeconds(timeToMove);
        action();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        clickable = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        clickable = false;
    }
}
