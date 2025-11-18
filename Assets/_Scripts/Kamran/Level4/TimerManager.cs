using RTLTMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour 
{
    public Action OnTimerUp;
    public int currentTime;
    [SerializeField] RTLTextMeshPro text;
    public bool isPaused;
    private void OnEnable()
    {
        RoadMapManager.Instance.onPauseGame += PauseTimer;  
        RoadMapManager.Instance.onResumeGame += ResumeTimer;
        TutorialManager.Instance.onPauseGame += PauseTimer;
        TutorialManager.Instance.onResumeGame += ResumeTimer;
    }
    private void OnDisable()
    {
        RoadMapManager.Instance.onPauseGame -= PauseTimer;
        RoadMapManager.Instance.onResumeGame -= ResumeTimer;
        TutorialManager.Instance.onPauseGame -= PauseTimer;
        TutorialManager.Instance.onResumeGame -= ResumeTimer;
    }

    private void ResumeTimer()
    {
        isPaused = false;
    }

    private void PauseTimer()
    {
        isPaused = true;
    }

    public void Init(int seconds)
    {
        StartTimer(seconds);
    }
    void StartTimer(int seconds)
    {
        currentTime = seconds;
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        text.text = currentTime.ToString();
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            if(!isPaused)
                currentTime--;
            text.text=currentTime.ToString();
        }
        OnTimerUp?.Invoke();
    }
}
