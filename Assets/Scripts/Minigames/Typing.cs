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
    [SerializeField] int timeLimit = 15;
    [SerializeField] string lettersPool = "ahj";
    [SerializeField] int wordLength = 5;
    [SerializeField] TMP_Text[] text;
    [SerializeField] int nextLetterIndex;
    [SerializeField] AudioClip typeSound;
    [SerializeField] AudioClip sendSound;
    [SerializeField] AudioClip recieveSound;
    [SerializeField] TMP_Text message1;
    [SerializeField] Transform emoji1;
    [SerializeField] Transform emoji2;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Sprite oneMessage;
    [SerializeField] Sprite twoMessage;
    [SerializeField] Sprite loseMessage;
    bool lost = false;
    string laugh;

    void Start()
    {
        GameManager.instance.StartStage(Color.black, timeLimit);
        nextLetterIndex = 0;
        laugh = GenerateRandomString();
        Debug.Log(laugh);
        for (int i = 0; i < laugh.Length; i++){
            text[i].text = laugh[i].ToString();
        }
    }

    void Keypress(char c){
        if (!lost) {
            if (nextLetterIndex < laugh.Length && (Char.IsLetter(c) || Char.IsDigit(c))) {
                char nextLetter = laugh[nextLetterIndex];
                Debug.Log("Next: " + nextLetter);
                if (c != nextLetter){
                    LoseStage();
                }else {
                    text[nextLetterIndex].color = Color.black;
                    text[nextLetterIndex].fontStyle = FontStyles.Bold;
                    AudioSource.PlayClipAtPoint(typeSound, Camera.main.transform.position, 0.3f);
                    nextLetterIndex++;
                }
                
                if (nextLetterIndex == laugh.Length){
                    WinStage();
                }
            }
        }
    }

    [ContextMenu("RandomString")]
    string GenerateRandomString() {
        var chars = Enumerable.Range(0, wordLength)
            .Select(x => lettersPool[Random.Range(0, lettersPool.Length)]);
        return new string(chars.ToArray());
    }

    private void LoseStage() {
        lost = true;
        for(int i=0; i < text.Length; i++) {
            text[i].text = "";
        }
        StartCoroutine(LoseRoutine());
    }

    private void WinStage() {
        GameManager.instance.WinStage();
        for(int i=0; i < text.Length; i++) {
            text[i].text = "";
        }
        StartCoroutine(SendReceive());
    }

    IEnumerator SendReceive() {
        AudioSource.PlayClipAtPoint(sendSound, Camera.main.transform.position, 0.3f);
        background.sprite = oneMessage;
        message1.text = laugh;
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(recieveSound, Camera.main.transform.position, 0.3f);
        background.sprite = twoMessage;
        emoji1.gameObject.SetActive(true);
    }

    IEnumerator LoseRoutine() {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(sendSound, Camera.main.transform.position, 0.3f);
        background.sprite = loseMessage;
        emoji2.gameObject.SetActive(true);
    }
}
