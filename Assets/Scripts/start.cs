using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    public void StartGame () {
        SceneManager.LoadScene("Hub");
    }
}
