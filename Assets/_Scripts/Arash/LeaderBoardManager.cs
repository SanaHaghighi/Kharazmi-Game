using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoardManager : SingletonBehaviour<LeaderBoardManager>
{
    [SerializeField] GameObject leaderboardObject;
    [SerializeField] GameObject leaderboardParent;
    [SerializeField] Button backBtn;
    [SerializeField] Button globalButton;
    [SerializeField] Button classButton;
    [SerializeField] Button profileButton;
    LeaderBoards leaderboards;
    bool isGlobal;
    private async void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Menu");
        });
        globalButton.onClick.AddListener(()=>GlobalButtonClickedAsync(false));
        classButton.onClick.AddListener(ClassButtonClickedAsync);
        profileButton.onClick.AddListener(ProfileButtonClicked);
        isGlobal = true;
        GlobalButtonClickedAsync(true);
    }

    private void ProfileButtonClicked()
    {
        PlayerPrefs.SetString("PreviousScene", "Leaderboard");
        SceneManager.LoadScene("Menu");
    }

    private async void ClassButtonClickedAsync()
    {
        if (isGlobal)
        {
            Debug.Log("getting class leaderboard");
            isGlobal = false;
            ClearLeaderBoard();
            await GetLocalLeaderboard();
        }
    }

    private async void GlobalButtonClickedAsync(bool isForce)
    {
        if (!isGlobal || isForce)
        {
            Debug.Log("getting global leaderboard");
            isGlobal=true;
            ClearLeaderBoard();
            await GetGloabalLeaderboard();
        }
    }
    async Task GetGloabalLeaderboard()
    {
        var res = await APIManager.Instance.GetLeaderboard(null, () =>
        {
            //PopupController.Instance.ShowPopup("Connection Error", "Error", "Ok");
        });
        Debug.Log(res);
        leaderboards = JsonUtility.FromJson<LeaderBoards>(res);
        Debug.Log(leaderboards.msg);
        MakeLeaderBoard(leaderboards.global_leaderboard);
    }
    async Task GetLocalLeaderboard()
    {
        var res = await APIManager.Instance.GetLeaderboard(null, () =>
        {
            //PopupController.Instance.ShowPopup("Connection Error", "Error", "Ok");
        });
        Debug.Log(res);
        leaderboards = JsonUtility.FromJson<LeaderBoards>(res);
        Debug.Log(leaderboards.msg);
        MakeLeaderBoard(leaderboards.class_leaderboard);
    }
    public void MakeLeaderBoard(LeaderBoardData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            var obj=Instantiate(leaderboardObject,parent: leaderboardParent.transform).GetComponent<LeaderboardObject>();
            obj.Setup(data[i].username, data[i].score_received);
        }        
    }
    public void ClearLeaderBoard()
    {
        for (int i = 0; i < leaderboardParent.transform.childCount; i++)
        {
            Destroy(leaderboardParent.transform.GetChild(i).gameObject);
        }
    }

}
[System.Serializable]
public class LeaderBoards
{
    public LeaderBoardData[] class_leaderboard;
    public LeaderBoardData[] global_leaderboard;
    public string msg;
}
[System.Serializable]
public class LeaderBoardData{
    public string username;
    public int score_received;
}
