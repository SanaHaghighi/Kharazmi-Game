using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static Unity.Burst.Intrinsics.X86.Avx;

public class APIManager : SingletonBehaviour<APIManager>
{
    [Serializable]
    public class SignUpRequest
    {
        public string username;
        public string password;
        public string email;
        public string class_name;
    }

    [Serializable]
    public class LoginRequest
    {
        public string username;
        public string password;
    }

    [Serializable]
    public class GameRequest
    {
        public int game_level;
        public int level1_score;
        public int level2_score;
        public int level3_score;
        public int level4_score;
    }
    private readonly string baseUrl = "http://127.0.0.1:5000"; // Replace with your actual server URL
    public static string JwtToken { get; private set; }
    private void Start()
    {
        JwtToken = PlayerPrefs.GetString("jwt_token", "nothing");
        Debug.Log("jwt token : ");
        Debug.Log(JwtToken);
        DontDestroyOnLoad(gameObject);
    }
#if UNITY_EDITOR
    [ContextMenu("Test")]
    public async void CallTestFunction()
    {
        string loginResponse = await Login(null,null,"arman", "123456");
        Debug.Log(loginResponse);
    }
#endif
    public Task<string> SignUp(Action OnSuccess, Action<string> OnFail, string username, string password, string email, string className)
    {
        string url = baseUrl + "/signup";
        var data = new SignUpRequest
        {
            username = username,
            password = password,
            email = email,
            class_name = className
        };
        var json = JsonUtility.ToJson(data);
        OnSuccess += () =>
        {
            Login(null, null, username, password);
        };
        return SendPostRequest(OnSuccess, OnFail, url, json);
    }
    [Serializable]
    private class LoginResponse
    {
        public string token;
        public string msg;
    }
    public async Task<string> Login(Action OnSuccess, Action<string> OnFail, string username, string password)
    {
        string url = baseUrl + "/login";
        var data = new LoginRequest { username = username, password = password };
        string responseJson = await SendPostRequest(OnSuccess,OnFail,url, JsonUtility.ToJson(data));

        if (!string.IsNullOrEmpty(responseJson))
        {
            try
            {
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(responseJson);
                if (!string.IsNullOrEmpty(response.token))
                {
                    JwtToken = response.token;
                    PlayerPrefs.SetString("jwt_token", JwtToken);
                    PlayerPrefs.Save();
                    Debug.Log("jwt token : ");
                    Debug.Log(JwtToken);
                }
                return response.msg;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to parse login response: " + ex.Message);
            }
        }
        return "Login failed!";
    }

    public Task<string> SendGameData(Action OnSuccess, Action<string> OnFail, int gameLevel, int level1, int level2, int level3, int level4)
    {
        string url = baseUrl + "/game";
        var data = new GameRequest
        {
            game_level = gameLevel,
            level1_score = level1,
            level2_score = level2,
            level3_score = level3,
            level4_score = level4
        };
        var json = JsonUtility.ToJson(data);
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {
                "Authorization","Bearer "+JwtToken
            }
        };
        return SendPostRequest(OnSuccess, OnFail, url, json,dic);
    }

    public Task<string> GetLeaderboard(Action OnSuccess, Action OnFail)
    {
        string url = baseUrl + "/leaderboard";
        Dictionary<string,string> dic= new Dictionary<string, string>()
        {
            {
                "Authorization","Bearer "+JwtToken
            }
        };
        return SendGetRequest(OnSuccess, OnFail, url,dic);
    }
    public Task<string> GetHighScores(Action OnSuccess, Action OnFail)
    {
        string url = baseUrl + "/max-game-levels";
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {
                "Authorization","Bearer "+JwtToken
            }
        };
        return SendGetRequest(OnSuccess, OnFail, url, dic);
    }
    public Task<string> GetGameCount(Action OnSuccess, Action OnFail)
    {
        string url = baseUrl + "/games-count";
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {
                "Authorization","Bearer "+JwtToken
            }
        };
        return SendGetRequest(OnSuccess, OnFail, url, dic);
    }
    public Task<string> GetMaxDifficulityScores(Action OnSuccess, Action OnFail)
    {
        string url = baseUrl + "/score-received";
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {
                "Authorization","Bearer "+JwtToken
            }
        };
        return SendGetRequest(OnSuccess, OnFail, url, dic);
    }
    public Task<string> GetRanks(Action OnSuccess, Action OnFail)
    {
        string url = baseUrl + "/get-ranks";
        Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {
                "Authorization","Bearer "+JwtToken
            }
        };
        return SendGetRequest(OnSuccess, OnFail, url, dic);
    }
    private Task<string> SendPostRequest(Action OnSuccess, Action<string> OnFail, string url, string jsonData, Dictionary<string, string> header = null)
    {
        var tcs = new TaskCompletionSource<string>();
        Debug.Log(jsonData);
        StartCoroutine(SendPostRequestCoroutine(OnSuccess, OnFail, url, jsonData, tcs, header));
        return tcs.Task;
    }
    private Task<string> SendGetRequest(Action OnSuccess, Action OnFail, string url, Dictionary<string, string> header = null)
    {
        var tcs = new TaskCompletionSource<string>();
        StartCoroutine(SendGetRequestCoroutine(OnSuccess, OnFail, url, tcs, header));
        return tcs.Task;
    }
    private IEnumerator SendPostRequestCoroutine(Action OnSuccess, Action<string> OnFail,string url, string jsonData, TaskCompletionSource<string> tcs, Dictionary<string,string> headers)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        // Only attach a body if jsonData is provided
        if (!string.IsNullOrEmpty(jsonData))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.SetRequestHeader("Content-Type", "application/json");
        }

        // Add custom headers if provided
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }
        }
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            OnSuccess?.Invoke();
            if (request.downloadHandler != null)
            {
                tcs.SetResult(request.downloadHandler.text);
            }
        }
        else
        {
            string tmp = "-";
            if (request.downloadHandler!=null)
            {
                tmp = request.downloadHandler.text;
            }
            var response = $"Error: {request.responseCode} - {tmp}";
            Debug.Log(response);
            tcs.SetResult(response);
            OnFail?.Invoke(response);
        }
    }
    private IEnumerator SendGetRequestCoroutine(Action OnSuccess, Action OnFail, string url, TaskCompletionSource<string> tcs, Dictionary<string, string> headers)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "Get");
        request.downloadHandler = new DownloadHandlerBuffer();

        // Add custom headers if provided
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }
        }
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            OnSuccess?.Invoke();
            if (request.downloadHandler != null)
            {
                tcs.SetResult(request.downloadHandler.text);
            }
        }
        else
        {
            OnFail?.Invoke();
            string tmp = "-";
            if (request.downloadHandler != null)
            {
                tmp = request.downloadHandler.text;
            }
            var response = $"Error: {request.responseCode} - {tmp}";
            Debug.Log(response);
            tcs.SetResult(response);
        }
    }
}
