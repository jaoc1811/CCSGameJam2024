using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] Mole mole;
    public bool clickable = false;


    void Click() {
        if (clickable) {
            mole.LoseStage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        clickable = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        clickable = false;
    }
}
