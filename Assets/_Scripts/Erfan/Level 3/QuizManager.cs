using System;
using UnityEngine;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using SFXSystem;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
using System.Threading.Tasks;

public class QuizManager : MonoBehaviour
{
    public EquationBank equationBank;
    public WeightsManager weightsManagerR;
    public WeightsManager weightsManagerL;
    public RectTransform scaleTop;

    public RTLTextMeshPro equationText;

    

    public RTLTextMeshPro[] Answer1Text;
    public RTLTextMeshPro[] Answer2Text;
    public RTLTextMeshPro[] Answer3Text;
    public RTLTextMeshPro[] Answer4Text;

    [SerializeField] private LevelHandler handler;
    
   

    [SerializeField] private GameObject secondQuestion;



    private int answerIndex;

    public RTLTextMeshPro timerText;
    
    

    private int currentLevel;
    private int correctAnswer = 0;
    private float timeRemaining;
    private bool isQuizActive;


    public int CurrentLevel => currentLevel;

    public int CorrectAnswer => correctAnswer;


    public bool IsQuizActive
    {
        get => isQuizActive;
        set => isQuizActive = value;
    }

    private void Start()
    {
        //StartQuiz();
        
    }
    public int AnswerIndex => answerIndex;

    public void StartQuiz()
    {

        currentLevel = 1;
        correctAnswer = 0;
        timeRemaining = 120+PlayerPrefs.GetInt("Level2",0)*10;
        isQuizActive = true;
        StartCoroutine(LoadNextQuestion());
        StartCoroutine(Timer());
    }

    Equation equation;
    private IEnumerator LoadNextQuestion()
    {
        yield return new WaitForSeconds(.2f);
        List<Equation> equations;

        var difficulty = PlayerPrefs.GetInt(SettingsManager.DIFFICULTY_KEY, 1);

        switch (difficulty)
        {
            case 1:
                equations = equationBank.easyEquations;
                break;
            case 2:
                equations = equationBank.mediumEquations;
                break;
            case 3:
                equations = equationBank.hardEquations;
                break;
            default:
                equations = new List<Equation>();
                break;
        }

        // Randomly select an equation
        int randomIndex = Random.Range(0, equations.Count);
        equation = equations[randomIndex];
        weightsManagerR.PutWeights(equation);
        weightsManagerL.PutWeights(equation);
        scaleTop.localEulerAngles = new Vector3(0, 0, 0);
        // Set the equation and answers
        equationText.text = equation.equationText;

        List<RTLTextMeshPro[]> Answerlist = new List<RTLTextMeshPro[]>();

        // adding the answers to a list
        Answerlist.Add(Answer1Text);
        Answerlist.Add(Answer2Text);
        Answerlist.Add(Answer3Text);
        Answerlist.Add(Answer4Text);

        // create 4 shuffled numbers from 0 to 3
        int[] shuffledArray = ShuffleFourNumbers();

        int[] ShuffleFourNumbers()
        {
            int[] numbers = { 0, 1, 2, 3 };
            for (int i = 0; i < numbers.Length; i++)
            {
                int randomIndex = Random.Range(0, numbers.Length);
                // Swap the numbers
                (numbers[i], numbers[randomIndex]) = (numbers[randomIndex], numbers[i]);
            }
            return numbers;
        }
        answerIndex = shuffledArray[0];
        for (int i = 0; i < 4; i++)
        {
            var cnt = i;
            Answerlist[answerIndex % 4][i].text = equation.correctAnswer[i];
            var btn = Answerlist[answerIndex % 4][i].transform.parent.transform.parent.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                PressedAnswer(0);
            }
            );
            btn.onClick.AddListener(() => StartCoroutine(handler.AnswerChecker(btn.gameObject)));
            Answerlist[(answerIndex + 1) % 4][i].text = equation.incorrectAnswer1[i];
            var btn2 = Answerlist[(answerIndex + 1) % 4][i].transform.parent.transform.parent.GetComponent<Button>();
            btn2.onClick.RemoveAllListeners();
            btn2.onClick.AddListener(() => {
                PressedAnswer(1);
            }
            );
            btn2.onClick.AddListener(() => StartCoroutine(handler.AnswerChecker(btn2.gameObject)));
            Answerlist[(answerIndex + 2) % 4][i].text = equation.incorrectAnswer2[i];
            var btn3 = Answerlist[(answerIndex + 2) % 4][i].transform.parent.transform.parent.GetComponent<Button>();
            btn3.onClick.RemoveAllListeners();
            btn3.onClick.AddListener(() => {
                PressedAnswer(2);
            }
            );
            btn3.onClick.AddListener(() => StartCoroutine(handler.AnswerChecker(btn3.gameObject)));
            Answerlist[(answerIndex + 3) % 4][i].text = equation.incorrectAnswer3[i];
            var btn4 = Answerlist[(answerIndex + 2) % 4][i].transform.parent.transform.parent.GetComponent<Button>();
            btn4.onClick.RemoveAllListeners();
            btn4.onClick.AddListener(() => {
                PressedAnswer(3);
                }
            );
            btn4.onClick.AddListener(() => StartCoroutine(handler.AnswerChecker(btn4.gameObject)));

            //Debug.Log(shuffledArray[i] + 1);

        }
    }
    void PressedAnswer(int val)
    {
        Debug.Log("val = " + val);
        var x = equation.incorrectAnswerLeftOrRight[val];
        if (x == 0) return;
        if (x > 0)
        {
            MoveScaleRight();
        }
        else
        {
            MoveScaleLeft();
        }
    }

    private void MoveScaleLeft()
    {
        scaleTop.DORotate(new Vector3(0, 0, 10), 1f);
        float targetY = weightsManagerL.Obj.anchoredPosition.y - 13;
        weightsManagerL.Obj.DOAnchorPosY(targetY, 1f);
        float targetY2 = weightsManagerR.Obj.anchoredPosition.y + 13;
        weightsManagerR.Obj.DOAnchorPosY(targetY2, 1f);
    }

    void MoveScaleRight()
    {
        scaleTop.DORotate(new Vector3(0, 0, -10), 1f);
        float targetY = weightsManagerL.Obj.anchoredPosition.y + 13;
        weightsManagerL.Obj.DOAnchorPosY(targetY, 1f);
        float targetY2 = weightsManagerR.Obj.anchoredPosition.y - 13;
        weightsManagerR.Obj.DOAnchorPosY(targetY2, 1f);
    }
    public void CheckAnswer(bool answer)
    {
        StreakHandler.Instance.Answered(answer);
        if (answer)
        {
            correctAnswer++;
        }
        currentLevel++;

        if (currentLevel > 3)
        {

            // End of quiz
            handler.EndQuiz(correctAnswer);
        }
        else
        {
            StartCoroutine(LoadNextQuestion());
            StartCoroutine(Timer());
        }
    }

    
    private IEnumerator Timer()
    {

        yield return new WaitForSeconds(0.5f);
        
        while (isQuizActive)
        {
            timeRemaining -= Time.deltaTime;

            // Update timer UI
            timerText.text = timeRemaining.ToString("F0");
            if (timeRemaining <= 0)
            {
                // Time's up
                handler.EndQuiz_Time(CorrectAnswer);
            }

            yield return null;
        }
    }
    
}
    
   