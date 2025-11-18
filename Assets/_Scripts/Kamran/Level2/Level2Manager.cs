using DG.Tweening;
using RTLTMPro;
using SFXSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] SpawnCollectibles spawnCollectibles;
    [SerializeField] PlayerManager player;
    [SerializeField] GameObject GameObj;
    [SerializeField] GameObject SelectionPanel;
    [SerializeField] GameObject WordSelection;
    [SerializeField] List<Button> Buttons;
    [SerializeField] Button StartGameButton;
    [SerializeField] GameObject Grid;
    [SerializeField] HeaderHandler Header;

    string key;
    private void Start()
    {
        Header.SetTimeObjectEnable(false);
        SoundSystemManager.Instance.ChangeBGM("Music");
        SoundSystemManager.Instance.PlayBGM();
        player.joystick.gameObject.SetActive(false);
        GameObj.SetActive(false);
        SelectionPanel.SetActive(true);
        WordSelection.SetActive(true);
        foreach (var b in Buttons)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() =>
            {
                SetKey(b.transform.GetChild(0).GetComponent<RTLTextMeshPro>().text);
            });
        }
        StartGameButton.onClick.RemoveAllListeners();
        StartGameButton.onClick.AddListener(()=>StartGame(key));
    }
    void SetKey(string key)
    {
        this.key = key;
    }
    void StartGame(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            Grid.transform.DOKill(true);
            Grid.transform.DOPunchScale(0.1f * Vector3.one, 0.25f);
            return;
        }
        SelectionPanel.SetActive(false);
        GameObj.SetActive(true);
        player.joystick.gameObject.SetActive(true);
        player.Init(key);
        spawnCollectibles.SpawnCollectible();
    }
    public static List<int> GetUniqueRandomNumbers(int count,int initialOveralMax)
    {
        List<int> numbers = new();
        System.Random rand = new();
        int min = 0;
        while (numbers.Count < count)
        {
            int tmp = rand.Next(0, 4);
            if (tmp != 0)
            {
                min = (initialOveralMax + numbers.Count+1) / 2;
            }
            int num = rand.Next(min, initialOveralMax + numbers.Count+1);
            min = 0;
            if (!numbers.Contains(num))
            {
                numbers.Add(num);
                numbers.Sort();
            }
        }

        return numbers;
    }
}
