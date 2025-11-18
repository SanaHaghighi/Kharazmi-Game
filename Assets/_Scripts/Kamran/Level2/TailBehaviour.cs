using DG.Tweening;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TailBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshPro tailKeyText;
    [SerializeField] RTLTextMeshPro3D tailPreText;
    [SerializeField] Transform bothTexts;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] bool isHead;
    public float delayTime = 1.0f;  
    public Queue<Vector3> positionHistory;  
    private float timeStep;
    bool isInit;
    public static bool isOdd;
    public void Init()
    {
        positionHistory = new Queue<Vector3>();
        positionHistory.Enqueue(transform.position);
        timeStep = Time.fixedDeltaTime;
        isInit = true;
        SetSprite(isOdd);
        isOdd = !isOdd;
    }
    void SetSprite(bool isRed)
    {
        if (isHead) return;
        if (isRed)
        {
            sprite.sprite = DatabaseHolder.Instance.RedTail;
        }
        else
        {
            sprite.sprite = DatabaseHolder.Instance.BlueTail;
        }
    }
    void FixedUpdate()
    {
        if (!isInit) return;
        if (!PlayerManager.Instance.IsMoving) return;
        positionHistory.Enqueue(transform.position);
        if (positionHistory.Count > delayTime / timeStep)
        {
            positionHistory.Dequeue();
        }
    }
    public void SetText(string _preText,string _keyText)
    {
        if(!_preText.Contains(",") && !_preText.Contains("----"))
            _preText = new string(_preText.Reverse().ToArray());

        tailKeyText.text = _keyText;
        tailPreText.text = _preText;


        if (_preText.Contains("----"))
        {
            tailPreText.characterSpacing = -14;
            tailPreText.wordSpacing = 10;
            tailPreText.lineSpacing = -55;
        }
        if (_preText.Length < 3 && !_preText.Contains("√"))
        {
            bothTexts.localPosition += new Vector3(-0.01f * (3 - _preText.Length), 0, 0);
        }
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * .2f, 1f);
    }
}
