using DG.Tweening;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro3D preText;
    [SerializeField] TextMeshPro suText;
    [SerializeField] Transform bothTexts;
    [HideInInspector]
    public string KeyString;
    [Header("Dotween Properties")]
    [SerializeField] float positionAmount = 2f;
    [SerializeField] float duration = 1f;
    [SerializeField] int vibrato = 1;
    [SerializeField] float rotationAmount = 90;
    [SerializeField] int elasticity = 1;
    [SerializeField] int delayMiliseconds = 1;
    [SerializeField] SpriteRenderer spriteRenderer;
    public void Init(string _keyString = "nothing")
    {
        SetRandomText(_keyString);
        StartCoroutine(Wiggle());
    }
    public void SetText(string _preTxt,string _suTxt, bool isConst=false)
    {
        preText.text = ConvertToFarsiText(_preTxt,preText);
        if (!isConst)
            suText.text = _suTxt;
        else
        {
            suText.text = "";
            bothTexts.localPosition += new Vector3(0.20f, 0, 0);
        }
        KeyString = _suTxt;
        if ((_preTxt.Length<3 || isConst) && !_preTxt.Contains("√"))
        {
            bothTexts.localPosition += new Vector3(-0.05f* (3-_preTxt.Length), 0, 0);
        }

        MakeRandomShape();
    }
    void MakeRandomShape()
    {
        var rand = Random.Range(0, DatabaseHolder.Instance.collectibleRandomSprites.Count);
        spriteRenderer.sprite = DatabaseHolder.Instance.collectibleRandomSprites[rand];
    }
    public static string ConvertToFarsiText(string _text, RTLTextMeshPro3D _preText)
    {
        string res = _text;
        if (_text.Contains("/"))
        {
            var newText = DatabaseHolder.Instance.DivisionFarsiFormat;
            if (_text.Contains("-"))
            {
                var replacing = _text[3];
                newText = newText.Replace('2', replacing);
            }
            else
            {
                var replacing = _text[2];
                newText = newText.Replace('2', replacing);
                newText = newText.Replace(" - ", "   ");
            }
            newText = newText.Substring(0, 3)
                 + "\n"
                 + newText.Substring(3 + 1);
            newText = newText.Substring(0, 12)
                 + "\n"
                 + newText.Substring(12 + 1);
            _preText.characterSpacing = -14;
            _preText.wordSpacing = 10;
            _preText.lineSpacing = -55;
            res = newText;
        }
        if (_text.Contains("."))
        {
            var newText = DatabaseHolder.Instance.DecimalFarsiFormat;
            if (_text.Contains("-"))
            {
                var replacing = _text[3];
                newText = newText.Replace("8,0", replacing+",0");
            }
            else
            {
                var replacing = _text[2];
                newText=newText.Replace("8,0", replacing + ",0");
                newText = newText.Replace(" - ", "");
            }
            res = newText;
        }
        return res;
    }
    void SetRandomText(string _keyString="nothing")
    {
        int rand = Random.Range(0, 5);
        if (rand == 0 && _keyString.Equals("nothing"))
        {
            rand = Random.Range(0, DatabaseHolder.Instance.Constants.Count);
            var constant=DatabaseHolder.Instance.Constants[rand];
            SetText(constant, constant,true);
            return;
        }
        rand = Random.Range(0,DatabaseHolder.Instance.KeyStrings.Count);
        var keyString=DatabaseHolder.Instance.KeyStrings[rand];
        if (!_keyString.Equals("nothing"))
        {
            keyString = _keyString;
        }
        else
        {
            while (keyString.Equals(PlayerManager.Instance.KeyString))
            {
                rand = Random.Range(0, DatabaseHolder.Instance.KeyStrings.Count);
                keyString = DatabaseHolder.Instance.KeyStrings[rand];
            }
        }
        rand=Random.Range(0,DatabaseHolder.Instance.KeyPrefixes.Count);
        var prefix=DatabaseHolder.Instance.KeyPrefixes[rand];
        SetText(prefix, keyString);
    }
    public string GetPreText()
    {
        return preText.text;
    }
    public string GetKeyText()
    {
        return suText.text;
    }
    IEnumerator Wiggle()
    {
        var delayAmountRand = Random.Range(0, delayMiliseconds);
        yield return new WaitForSeconds((float)delayAmountRand/1000);
        var positionAmountRand = Random.Range(1f, positionAmount);
        var rotationAmountRand = Random.Range(5f, rotationAmount);

        transform.DOShakePosition(duration, positionAmountRand, vibrato, elasticity, false, true, ShakeRandomnessMode.Harmonic)
            .SetLoops(-1);
        Vector3 rotationStrength = new Vector3(0f, 0f, rotationAmountRand);
        transform.DOShakeRotation(duration, rotationStrength, vibrato, elasticity, true, ShakeRandomnessMode.Harmonic)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
