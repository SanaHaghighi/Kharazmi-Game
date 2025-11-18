using System;
using RTLTMPro;
using UnityEngine;

public class StatObject : MonoBehaviour
{
    public RTLTextMeshPro difText;
    public RTLTextMeshPro scoreText;

    public void Setup(String difficulty, String score){
        difText.text="سختی: "+difficulty;
        scoreText.text="امتیاز "+score;
    }
}
