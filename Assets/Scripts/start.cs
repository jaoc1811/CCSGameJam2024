using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{

    public void StartGame () {
        SceneManager.LoadScene("Hub");
    }
}
