using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public int lives;

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
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameStarting = true;
        currentMinigames = new Queue<string>();
    }

    void Update()
    {
        
    }

    public void LoadStage(string stage){
        SceneManager.LoadScene(stage);
    }

    public void StartStage(){
        Debug.Log("Starting stage");
        StageCleared = false;
        Time = initialTime;
        timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void EndStage(){
        Debug.Log("Ending stage");
        GameStarting = false;
        SceneManager.LoadScene(main);
    }

    [ContextMenu("WinStage")]
    public void WinStage(){
        Debug.Log("Win stage");
        StopCoroutine(timerCoroutine);
        StageCleared = true;
        EndStage();
    }

    IEnumerator TimerRoutine() {
        while(Time >= 0) {
            Debug.Log(Time);
            yield return new WaitForSeconds(1);
            Time--;
        }
        Debug.Log("Lose stage");
        EndStage();
    }

    [ContextMenu("SelectNextMinigame")]
    void SelectNextMinigame() {
        if (currentMinigames.Count == 0){
            ShuffleMinigames();
            currentMinigames = new Queue<string>(nextMinigames);
        }
        string nextMinigame = currentMinigames.Dequeue();
        LoadStage(nextMinigame);
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
