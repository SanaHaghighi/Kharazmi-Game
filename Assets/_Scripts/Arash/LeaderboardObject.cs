using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardObject : MonoBehaviour
{
    public RTLTextMeshPro Name;
    public RTLTextMeshPro Rank;


    public void Setup(String name,int rank){
        Name.text=name;
        Rank.text = rank.ToString();

    }
}
