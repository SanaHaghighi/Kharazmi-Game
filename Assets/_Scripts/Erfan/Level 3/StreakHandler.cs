using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StreakHandler : SingletonBehaviour<StreakHandler>
{
    [SerializeField] public List<Image> Images;
    [SerializeField] Color acceptColor = Color.green;
    [SerializeField] Color failColor = Color.red;
    int cnt = 0;
    public void Answered(bool wasAccepted)
    {
        Images[cnt].color = wasAccepted ? acceptColor : failColor;
        cnt++;
    }
}
