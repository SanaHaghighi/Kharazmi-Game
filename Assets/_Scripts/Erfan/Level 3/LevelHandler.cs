using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using SFXSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject quiz;
    [SerializeField] private GameObject firstQuestion;
    [SerializeField] private GameObject secondQuestion;
    [SerializeField] private GameObject rightAnswer;
    [SerializeField] private GameObject wrongAnswer;
    [SerializeField] private GameObject EquationsParent;
    [SerializeField] private QuizManager manager;
    //[SerializeField] private GameObject statistics;


    private void Start()
    {
        quiz.SetActive(false);
        firstQuestion.SetActive(true);
        secondQuestion.SetActive(false);
        EquationsParent.SetActive(true);
        rightAnswer.SetActive(false);
        wrongAnswer.SetActive(false);
        SoundSystemManager.Instance.ChangeBGM("Music2");
        SoundSystemManager.Instance.PlayBGM();
        //statistics.SetActive(false);
    }


    public void NextLevel(int correct)
    {
        PlayerPrefs.SetInt("Level3", correct);
        PlayerPrefs.SetInt("Level3KH", correct);
        SceneManager.LoadScene("Score 3");
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuizLoader()
    {
        
        firstQuestion.SetActive(false);
        quiz.SetActive(true);
        
        manager.StartQuiz();
    }

    public void NextQuestion()
    {
        secondQuestion.SetActive(false);
        EquationsParent.SetActive(true);
    }
    
    
    public void EndQuiz(int correct)
    {
        manager.IsQuizActive = false;
        quiz.SetActive(false);
        NextLevel(correct);
    }
    
    public void EndQuiz_Time(int correct)
    {
        manager.IsQuizActive = false;
        quiz.SetActive(false);
        NextLevel(correct);
    }



    public IEnumerator AnswerChecker(GameObject checkedAnswer)
    {
        manager.IsQuizActive = false;
        RoadMapManager.Instance.onPauseGame?.Invoke();
        if (manager.AnswerIndex.ToString().Equals(checkedAnswer.tag))
        {
            if (manager.CurrentLevel >= 3)
            {
                rightAnswer.SetActive(true);
                var tmp = rightAnswer.GetComponent<CanvasGroup>();
                tmp.alpha = 0;
                tmp.DOFade(0.5f, 1f); // Lower alpha to 0.5 over 1 second
                yield return new WaitForSeconds(2);
                var tmp2 = rightAnswer.GetComponent<CanvasGroup>();
                tmp2.DOFade(0f, 1f).OnComplete(() =>
                {
                    rightAnswer.SetActive(false);
                }); // Lower alpha to 0.5 over 1 second
            }
            else
            {
                rightAnswer.SetActive(true);
                var tmp = rightAnswer.GetComponent<CanvasGroup>();
                tmp.alpha = 0;
                tmp.DOFade(0.5f, 1f); // Lower alpha to 0.5 over 1 second
                yield return new WaitForSeconds(2);
                var tmp2 = rightAnswer.GetComponent<CanvasGroup>();
                tmp2.DOFade(0f, 1f).OnComplete(() =>
                {
                    rightAnswer.SetActive(false);
                }); // Lower alpha to 0.5 over 1 second
                //secondQuestion.SetActive(true);
                //EquationsParent.SetActive(false);
            }

            manager.CheckAnswer(true);
            

        }
        else
        {
            if (manager.CurrentLevel >= 3)
            {
                wrongAnswer.SetActive(true);
                var tmp = wrongAnswer.GetComponent<CanvasGroup>();
                tmp.alpha = 0;
                tmp.DOFade(0.5f, 1f); // Lower alpha to 0.5 over 1 second
                yield return new WaitForSeconds(2);
                var tmp2 = wrongAnswer.GetComponent<CanvasGroup>();
                tmp2.DOFade(0f, 1f).OnComplete(() =>
                {
                    wrongAnswer.SetActive(false);
                }); // Lower alpha to 0.5 over 1 second
            }
            else
            {
                wrongAnswer.SetActive(true);
                var tmp = wrongAnswer.GetComponent<CanvasGroup>();
                tmp.alpha = 0;
                tmp.DOFade(0.5f, 1f); // Lower alpha to 0.5 over 1 second
                yield return new WaitForSeconds(2);
                var tmp2 = wrongAnswer.GetComponent<CanvasGroup>();
                tmp2.DOFade(0f, 1f).OnComplete(() =>
                {
                    wrongAnswer.SetActive(false);
                }); // Lower alpha to 0.5 over 1 second
                //secondQuestion.SetActive(true);
                //EquationsParent.SetActive(false);
            }

            manager.CheckAnswer(false);

            
            
        }
        
        manager.IsQuizActive = true;


    }

}
