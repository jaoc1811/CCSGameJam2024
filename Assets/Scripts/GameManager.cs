using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public int lives;
    [SerializeField] int stagesCleared = 0;

    [Header("Stage")]
    [SerializeField] bool stageCleared = false;
    public bool StageCleared {
        get => stageCleared;
        set {
            stageCleared = value;
        }
    }
    [SerializeField] bool gameStarting = true;
    public bool GameStarting {
        get => gameStarting;
        set {
            gameStarting = value;
        }
    }

    [Header("Scenes")]
    [SerializeField] string menu;
    [SerializeField] string main;
    [SerializeField] string gameOver;
    [SerializeField] Queue<string> currentMinigames;
    [SerializeField] List<string> nextMinigames;

    [Header("Timer")]
    [SerializeField] int initialTime = 20;
    [SerializeField] int time = 20;
    [SerializeField] int selectTimer = 2;

    [Header("Curtains")]
    [SerializeField] RectTransform curtains;
    [SerializeField] float curtainsTimer;

    Coroutine timerCoroutine = null;

    public int Time {
        get => time;
        set {
            time = value;
            // TODO: update UI
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(curtains.parent);
        GameStarting = true;
        currentMinigames = new Queue<string>();
        StartCoroutine(SelectNextgame());
    }

    public void LoadStage(string stage){
        SceneManager.LoadScene(stage);
    }

    public void StartStage(int stageTime = -1){
        Debug.Log("Starting stage");
        if (stageTime < 0) stageTime = initialTime;
        StageCleared = false;
        Time = stageTime;
        timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void EndStage(){
        Debug.Log("Ending stage");
        GameStarting = false;
        stagesCleared += 1;
        StartCoroutine(LoadStageWithCurtains(main, true));
    }

    [ContextMenu("WinStage")]
    public void WinStage(){
        Debug.Log("Win stage");
        StageCleared = true;
    }

    IEnumerator TimerRoutine() {
        while(Time >= 0) {
            Debug.Log(Time);
            yield return new WaitForSeconds(1);
            Time--;
        }
        EndStage();
    }

    IEnumerator LoadStageWithCurtains(string stage, bool hub = false) {
        StartCoroutine(curtainsTimer.Tweeng((p) => curtains.localPosition = p, new Vector3(0, 625, 0), new Vector3(0, 0, 0)));
        Debug.Log("DOWN CURTAINS");
        yield return new WaitForSeconds(curtainsTimer);
        LoadStage(stage);
        StartCoroutine(curtainsTimer.Tweeng((p) => curtains.localPosition = p, new Vector3(0, 0, 0), new Vector3(0, 625, 0)));
        Debug.Log("UP CURTAINS");
        yield return new WaitForSeconds(curtainsTimer);
        if (hub) {
            StartCoroutine(SelectNextgame());
        }
    }

    IEnumerator SelectNextgame() {
        if (!gameStarting) {
            if (stageCleared) {
                // TODO: Trigger happy animation
                Debug.Log("SUCCESS");
            } else {
                Debug.Log("FAIL");
                loseLife();
                // TODO: trigger sad animation
            }
        }
        yield return new WaitForSeconds(selectTimer);
        SelectNextMinigame();
    }

    void loseLife() {
        lives -= 1 ;
        Transform livesSprites = GameObject.Find("Lives").transform;
        livesSprites.GetChild(lives).GetComponent<Animator>().enabled = true;
        if (lives <= 0) {
            // trigger game over
        }
    }

    [ContextMenu("SelectNextMinigame")]
    void SelectNextMinigame() {
        if (currentMinigames.Count == 0){
            ShuffleMinigames();
            currentMinigames = new Queue<string>(nextMinigames);
        }
        string nextMinigame = currentMinigames.Dequeue();
        StartCoroutine(LoadStageWithCurtains(nextMinigame));
    }


    void ShuffleMinigames() {
        int count = nextMinigames.Count;
        int last = count - 1;
        for (int i = 0; i < last; ++i) {
            int r = Random.Range(i, count);
            string tmp = nextMinigames[i];
            nextMinigames[i] = nextMinigames[r];
            nextMinigames[r] = tmp;
        }
    }
}
