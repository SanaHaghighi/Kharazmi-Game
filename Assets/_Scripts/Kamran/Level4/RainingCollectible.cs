using DG.Tweening;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RainingCollectible : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] RTLTextMeshPro3D keyText;
    [SerializeField] SpriteRenderer shape;
    public string KeyText => keyText.text;
    public void Init(float endY)
    {
        SetText();
        shape.sprite = DatabaseHolder4.Instance.RainSprites[UnityEngine.Random.Range(0, DatabaseHolder4.Instance.RainSprites.Count)];
        transform.DOMoveY(endY, speed+Random.Range(-0.2f,0.2f)).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            RainingCollectibleManager.Instance.RemoveRain(this);
        });
    }

    private void SetText()
    {
        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            keyText.text = DatabaseHolder4.Instance.Constants[Random.Range(0, DatabaseHolder4.Instance.Constants.Count - 1)];
            return;
        }
        keyText.text = DatabaseHolder4.Instance.Prefixes[Random.Range(0, DatabaseHolder4.Instance.Prefixes.Count - 1)];
        keyText.text += "x";

    }
}
