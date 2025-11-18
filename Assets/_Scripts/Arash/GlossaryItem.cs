using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryItem : MonoBehaviour
{
    public RTLTextMeshPro Text;
    public Image image;

    public void Setup(String text, Color color){
        Text.text=text;
        image.color=color;
    }
}
