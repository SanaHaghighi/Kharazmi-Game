using DG.Tweening;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4Manager : SingletonBehaviour<Level4Manager>
{
    public RainingCollectibleManager rainManager;
    public ClickedThingsManager thingsManager;
    public TimerManager timerManager;
    public System.Action<RainingCollectible,int,bool> ClickedRain;
    public System.Action FinishedEquation;
    [SerializeField] Transform EquationSection;
    [SerializeField] RTLTextMeshPro EquationKeyText;
    [SerializeField] RTLTextMeshPro EquationSignText;
    [SerializeField] RTLTextMeshPro EquationSuffixText;
    [SerializeField] SolvedEquationManager solvedEquationPrefab;
    [SerializeField] Transform solvedEquationParent;
    [SerializeField] GameObject initPanel;
    public int EquationKeyNumber;
    public int EquationSufNumber;
    int totalSumKey;
    int totalSumSuf;
    public int score;
    bool isFirstEq;
    void Start()
    {
    }
    public void Init()
    {
        initPanel.SetActive(false);
        isFirstEq = true;
        score = 0;
        totalSumKey = 0;
        totalSumSuf = 0;
        rainManager.Init();
        thingsManager.Init();
        SetTime();
        GetRandomEquation();
    }
    void SetTime()
    {
        int time = 120;
        if (PlayerPrefs.HasKey("Level2"))
        {
            time += PlayerPrefs.GetInt("Level2") * 10;
        }
        if (PlayerPrefs.HasKey("Level3"))
        {
            time += PlayerPrefs.GetInt("Level3") * 20;
        }
        timerManager.Init(time);
        timerManager.OnTimerUp += GameOver;
    }
    public static string ReverseString(string input)
    {
        // Convert the string to a character array, reverse it, and return as a new string
        char[] charArray = input.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }
    public void GetRandomEquation()
    {
        var random = DatabaseHolder4.Instance.EquationPrefix[Random.Range(0, DatabaseHolder4.Instance.EquationPrefix.Count - 1)];
        var sign = "";
        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            sign = "-";
        }
        EquationKeyNumber = int.Parse(random);
        if (EquationKeyNumber == 10)
        {
            random = "01";
        }
        if (sign.Equals("-"))
        {
            EquationKeyNumber *= -1;
        }
        if (random.Equals("1"))
        {
            EquationKeyText.transform.position += new Vector3(0, 0.025f, 0);
            random = "";
        }
        EquationKeyText.text = random+ sign;
        EquationKeyText.text += "x";


        var newText = "+";
        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            newText = "-";
        }
        EquationSignText.text = newText;

        random = DatabaseHolder4.Instance.EquationSuffix[Random.Range(0, DatabaseHolder4.Instance.EquationSuffix.Count - 1)];
        EquationSuffixText.text = random;
        EquationSufNumber = int.Parse(random);
        if (newText.Equals("-"))
        {
            EquationSufNumber *= -1;
        }
        Debug.Log("random equation is " + EquationKeyNumber + " x "+ EquationSignText.text + " "+ EquationSufNumber);
        isFirstEq = false;
    }

    public void ClickedOnRain(RainingCollectible rain)
    {
        var tmpSuf = totalSumSuf;
        var tmpKey = totalSumKey;
        bool isNegetive = false;
        var txt =string.Copy(rain.KeyText);
        Debug.Log("doing logic for " + txt);
        if (txt.Contains("-"))
        {
            isNegetive = true;
            txt=txt.Replace("-","");
            Debug.Log("removing negetive : " + txt);
        }
        bool isKey = false;
        if (txt.Contains("x"))
        {
            txt = txt.Replace("x","");
            if (txt.Equals(""))
            {
                txt = "۱";
            }
            Debug.Log("converting key " + txt);
            var num = GetEnglishNumber(txt);
            if (isNegetive) num *= -1;
            isKey= true;
            totalSumKey += num;
        }
        else
        {
            Debug.Log("converting constant " + txt);
            if (txt.Equals(""))
            {
                txt = "۱";
            }
            var num = GetEnglishNumber(txt);
            if (isNegetive) num *= -1;
            totalSumSuf += num;
        }
        Debug.Log("THE TOTAL SUMS ARE : " + totalSumKey + " & " + totalSumSuf);
        ClickedRain?.Invoke(rain,isKey ? totalSumKey-tmpKey : totalSumSuf-tmpSuf,isKey);
        if (totalSumSuf == EquationSufNumber && totalSumKey == EquationKeyNumber)
        {
            EquationSolved();
        }
    }
    void EquationSolved()
    {
        var newSolvedEquation = Instantiate(solvedEquationPrefab, solvedEquationParent);
        newSolvedEquation.Repaint(EquationKeyText.text, EquationSignText.text, EquationSuffixText.text);
        score++;
        Debug.Log("YOU WIN THE EQUATION");
        totalSumSuf = 0;
        totalSumKey = 0;
        GetRandomEquation();
        EquationSection.DOKill(true);
        EquationSection.DOPunchScale(EquationSection.localScale * 0.2f, 0.25f);
        FinishedEquation?.Invoke();
    }
    int GetEnglishNumber(string text)
    {
        switch (text)
        {
            case "۱": return 1;
            case "۲": return 2;
            case "۳": return 3;
            case "۴": return 4;
            case "۵": return 5;
            case "۶": return 6;
            case "۷": return 7;
            case "۸": return 8;
            case "۹": return 9;
            case "۰۱": return 10;
            default: return 0;
        }
    }
    public void RemoveSelectedRain(ClickedThing thing)
    {

        if (thing.isKey)
        {
            totalSumKey -= thing.value;
        }
        else
        {
            totalSumSuf -= thing.value;
        }
        if (totalSumSuf == EquationSufNumber && totalSumKey == EquationKeyNumber)
        {
            EquationSolved();
        }
        thing.transform.DOPunchScale(thing.transform.localScale * 0.1f, 0.2f).OnComplete(() =>
        {
            Destroy(thing.gameObject);
        });
    }
    public void GameOver()
    {
        Debug.Log("TIME IS UP");
        Debug.Log("Score is " + score);
        RainingCollectibleManager.Instance.StartRaining = false;
        var sceneName = "Score 4";
        /*if (kharazmi >= 5)
        {
            sceneName = "Victory";
        }*/
        PlayerPrefs.SetInt("Level4", score);
        PlayerPrefs.SetInt("Level4KH", score);
        SceneManager.LoadScene(sceneName);
    }
}
