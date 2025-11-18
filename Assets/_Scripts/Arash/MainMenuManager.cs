using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : SingletonBehaviour<MainMenuManager>
{
    [System.Serializable]
    public class HighScores
    {
        public int max_level1_score;
        public int max_level2_score;
        public int max_level3_score;
        public int max_level4_score;
        public string msg;
    }
    [System.Serializable]
    public class Ranks
    {
        public int class_rank;
        public int global_rank;
        public string msg;
    }
    [System.Serializable]
    public class DifficulityScores
    {
        public int max_easy;
        public int max_medium;
        public int max_hard;
        public int score_received;
        public string msg;
    }
    [System.Serializable]
    public class GameCount
    {
        public int games_count;
        public string msg;
    }
    [SerializeField] GameObject MenuPanel;
    [SerializeField] SettingsManager SettingsPanel;
    
    [SerializeField] GameObject ProfilePanel;

    [SerializeField] Button StartButton;
    [SerializeField] Button TutorialButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ProfileButton;

    [SerializeField] Button SettingsBackButton;
    [SerializeField] Button ProfileBackButton;
    [SerializeField] ScrollRect ProfileScroll;
    [SerializeField] ProfileManager profileManager;
    void Start()
    {
        
        ProfilePanel.SetActive(false);
        MenuPanel.SetActive(true);
        if (PlayerPrefs.GetString("PreviousScene") == "Leaderboard")
        {
            PlayerPrefs.DeleteKey("PreviousScene");
            MenuPanel.SetActive(false);
            ProfilePanel.SetActive(true);
        }
        TutorialButton.onClick.AddListener(TutorialButtonClicked);
        
        SettingsButton.onClick.AddListener(SettingsButtonClicked);
        SettingsBackButton.onClick.AddListener(SettingsBackButtonClicked);
        ProfileButton.onClick.AddListener(ProfileButtonClicked);
        ProfileBackButton.onClick.AddListener(ProfileBackButtonClicked);
        //TODO Other Button Interactions
        StartButton.onClick.AddListener(StartGameClicked);
        GetHighScores();
        SettingsPanel.LoadSettings();
    }

    private async void GetHighScores()
    {
        var res=await APIManager.Instance.GetHighScores(null,null);
        Debug.Log(res);
        var hs = JsonUtility.FromJson<HighScores>(res);
        profileManager.SetSize(1, hs.max_level1_score * 10);
        profileManager.SetSize(2, hs.max_level2_score * 100 / 3);
        profileManager.SetSize(3, hs.max_level3_score * 10);
        profileManager.SetSize(4, hs.max_level4_score * 50);
        var res2= await APIManager.Instance.GetGameCount(null,null);
        var res4 = await APIManager.Instance.GetMaxDifficulityScores(null, null);
        var res3 = await APIManager.Instance.GetRanks(null, null);
        var gameCount = JsonUtility.FromJson<GameCount>(res2);
        var ranks = JsonUtility.FromJson<Ranks>(res3);
        var difficulityScores = JsonUtility.FromJson<DifficulityScores>(res4);
        profileManager.globalRank.text = ranks.global_rank.ToString();
        profileManager.classRank.text = ranks.class_rank.ToString();
        profileManager.points.text= difficulityScores.score_received.ToString();
        profileManager.easyStat.Setup("آسان", difficulityScores.max_easy.ToString());
        profileManager.mediumStat.Setup("متوسط", difficulityScores.max_medium.ToString());
        profileManager.hardStat.Setup("سخت", difficulityScores.max_hard.ToString());
        profileManager.difficultyPointsText.text = "ایول تا اینجا " + gameCount.games_count + " دست بازی کردی";

        //TODO HighScores figure KAMRAN
    }

    private void ProfileBackButtonClicked()
    {
        ProfilePanel.SetActive(false);
        ProfileScroll.verticalNormalizedPosition = 1;
        MenuPanel.SetActive(true);
    }

    private void ProfileButtonClicked()
    {
        MenuPanel.SetActive(false);
        ProfileScroll.verticalNormalizedPosition = 1;
        ProfilePanel.SetActive(true);
    }

    private void StartGameClicked()
    {
        SceneManager.LoadScene("Level 2");
    }

    private void SettingsBackButtonClicked()
    {
        SettingsPanel.gameObject.SetActive(false);
        MenuPanel.SetActive(true);
    }

    private void SettingsButtonClicked()
    {
        SettingsPanel.gameObject.SetActive(true);
        MenuPanel.SetActive(false);
    }


    //TODO set Tutorial Texts
    

    

    private void TutorialButtonClicked()
    {
        TutorialManager.Instance.OpenTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
