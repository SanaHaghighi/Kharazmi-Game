using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelRecordObject : MonoBehaviour
{
    public RectTransform rectTransform;
    public RTLTextMeshPro recordText;
    public Image image;
    public void Setup(Color color,String text,float height){
        recordText.text=text;
        rectTransform.sizeDelta=new Vector2(rectTransform.sizeDelta.x,height);
        image.color=color;
    }
}
