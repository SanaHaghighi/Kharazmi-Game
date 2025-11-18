using System;
using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class TextHandler : MonoBehaviour
{
    // Reference to RTLTextMeshPro components
    public RTLTextMeshPro[] rtlText;

    [SerializeField] private RTLTextMeshPro[] text;

    private Level4Manager manager;


    // String fields to manually input text
    [TextArea(3, 10)]
    public string[] text1;
    


    private void Update()
    {
        
        
        text[0].text = (PlayerPrefs.GetInt("Level2") * 5 + PlayerPrefs.GetInt("Level3") * 10 ).ToString();
    }


    private void Start()
    {
        text[1].text = PlayerPrefs.GetInt("Level2").ToString();
        text[2].text = PlayerPrefs.GetInt("Level3").ToString();
        text[3].text = PlayerPrefs.GetInt("Level4").ToString();


    }


    // This function will be called whenever the script is updated in the editor
    void OnValidate()
    {
        // Check if all text components are assigned and set the corresponding text
        for (int i = 0; i < rtlText.Length; i++)
        {
            if (rtlText[i] != null)
            {
                rtlText[i].text = text1[i];
            }
        }
    }
}
