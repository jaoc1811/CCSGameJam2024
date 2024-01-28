using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

using Random = UnityEngine.Random;

public class Typing : MonoBehaviour
{
    [SerializeField] string lettersPool = "ahj";
    [SerializeField] int wordLength = 5;
    [SerializeField] TMP_Text[] text;
    [SerializeField] int nextLetterIndex;
    [SerializeField] AudioClip typeSound;
    [SerializeField] AudioClip sendSound;
    [SerializeField] AudioClip recieveSound;
    string laugh;

    void Start()
    {
        GameManager.instance.StartStage();
        nextLetterIndex = 0;
        laugh = GenerateRandomString();
        Debug.Log(laugh);
        for (int i = 0; i < laugh.Length; i++){
            text[i].text = laugh[i].ToString();
        }
    }

    void Keypress(char c){
        if (nextLetterIndex < laugh.Length && (Char.IsLetter(c) || Char.IsDigit(c))) {
            char nextLetter = laugh[nextLetterIndex];
            Debug.Log("Next: " + nextLetter);
            if (c != nextLetter){
                Debug.Log("Lose");
            }else {
                text[nextLetterIndex].color = Color.black;
                AudioSource.PlayClipAtPoint(typeSound, Camera.main.transform.position, 0.3f);
                nextLetterIndex++;
            }
            
            if (nextLetterIndex == laugh.Length){
                WinStage();
            }
        }
    }

    [ContextMenu("RandomString")]
    string GenerateRandomString() {
        var chars = Enumerable.Range(0, wordLength)
            .Select(x => lettersPool[Random.Range(0, lettersPool.Length)]);
        return new string(chars.ToArray());
    }

    private void WinStage() {
        GameManager.instance.WinStage();
        StartCoroutine(SendReceive());
    }

    IEnumerator SendReceive() {
        AudioSource.PlayClipAtPoint(sendSound, Camera.main.transform.position, 0.3f);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(recieveSound, Camera.main.transform.position, 0.3f);
    }
}
