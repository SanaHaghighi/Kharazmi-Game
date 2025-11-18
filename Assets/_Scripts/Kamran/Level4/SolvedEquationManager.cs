using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolvedEquationManager : MonoBehaviour
{

    [SerializeField] RTLTextMeshPro EquationKeyText;
    [SerializeField] RTLTextMeshPro EquationSignText;
    [SerializeField] RTLTextMeshPro EquationSuffixText;
    public void Repaint(string EquationKeyText,string EquationSignText,string EquationSuffixText)
    {

        this.EquationKeyText.text = Level4Manager.ReverseString(EquationKeyText);
        this.EquationSignText.text = Level4Manager.ReverseString(EquationSignText);
        this.EquationSuffixText.text = Level4Manager.ReverseString(EquationSuffixText);
    }
}
