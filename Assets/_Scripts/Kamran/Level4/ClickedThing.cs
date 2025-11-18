using RTLTMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedThing : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro text;
    [SerializeField] Button button;
    public int value;
    public bool isKey;
    public void Repaint(string text, int _value, bool _isKey)
    {
        value = _value;
        isKey = _isKey;
        text = ReverseString(text);
        this.text.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Debug.Log("Removing " + value + " as a key = " + isKey);
            Level4Manager.Instance.RemoveSelectedRain(this);
        });
    }

    static string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
