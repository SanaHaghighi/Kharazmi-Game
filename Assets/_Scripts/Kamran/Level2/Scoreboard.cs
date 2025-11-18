using RTLTMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro scoreText;
    [SerializeField] RTLTextMeshPro scorePercent;
    //[SerializeField] RTLTextMeshPro percentage;
    [SerializeField] Image fillSprite;
    [SerializeField] int level;
    [SerializeField] string nextLevelName;
    public void GoNext()
    {
        SceneManager.LoadScene(nextLevelName);
    }
    private void Start()
    {
        float maxScore = level switch
        {
            3 => 3,
            4 => 10,
            _ => 10,
        };
        float scoreMult = level switch
        {
            2 => 10,
            3 => 20,
            4 => 40,
            _ => 10,
        };
        var score= PlayerPrefs.GetInt("Level"+level);
        scoreText.text = (scoreMult*score).ToString();
        var percent = (float)score / maxScore;
        scorePercent.text = ((int)(percent*100)).ToString() + " %";
        fillSprite.fillAmount = percent;
        //percentage.text = ((score*100)/maxScore).ToString() + " %";
    }
}
