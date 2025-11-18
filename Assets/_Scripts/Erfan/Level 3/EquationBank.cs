using System;
using System.Collections;
using System.Collections.Generic;
using RTLTMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquationBank", menuName = "Create/Equation Bank")]
public class EquationBank : ScriptableObject
{
    public List<Equation> easyEquations;
    public List<Equation> mediumEquations;
    public List<Equation> hardEquations;
}

[System.Serializable]
public struct Equation
{
    public string equationText;
    public String[] correctAnswer;
    public String[] incorrectAnswer1;
    public String[] incorrectAnswer2;
    public String[] incorrectAnswer3;
    public String[] encapsulatingXR;
    public int numberXR;
    public int numberR;
    public String[] encapsulatingXL;
    public int numberXL;
    public int numberL;

    public int[] incorrectAnswerLeftOrRight;
}



