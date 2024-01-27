using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

using Random = UnityEngine.Random;
using TMPro;

public class Typing : MonoBehaviour
{
    [SerializeField] string lettersPool = "ahj";
    [SerializeField] int wordLength = 5;
    [SerializeField] TMP_Text[] text;
    [SerializeField] int nextLetterIndex;
    string laugh;

    void Start()
    {
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
                nextLetterIndex++;
            }
            
            if (nextLetterIndex == laugh.Length){
                Debug.Log("Win");
            }
        }
    }

    [ContextMenu("RandomString")]
    string GenerateRandomString() {
        var chars = Enumerable.Range(0, wordLength)
            .Select(x => lettersPool[Random.Range(0, lettersPool.Length)]);
        return new string(chars.ToArray());
    }
}
