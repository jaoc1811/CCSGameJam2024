using UnityEngine;

public class Roses : MonoBehaviour
{
    [SerializeField] int targetRoses = 5;
    [SerializeField] int currentRoses = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        currentRoses += 1;
        other.gameObject.GetComponent<Dragable>().deactivate();
        if (currentRoses >= targetRoses) {
            WinStage();
        }
    }

    private void WinStage() {
        // TODO: Trigger success animation/sounds
        GameManager.instance.WinStage();
    }
}
