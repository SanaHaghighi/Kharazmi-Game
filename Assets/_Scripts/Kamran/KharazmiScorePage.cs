using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KharazmiScorePage : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] string nextLevelName;
    [SerializeField] Transform GridParent;
    [SerializeField] Image Prefab;
    private void Start()
    {
        foreach(Transform t in GridParent)
        {
            Destroy(t.gameObject);
        }
        var score = PlayerPrefs.GetInt("Level" + level+"KH");
        for(int i = 0; i < score; i++)
        {
            Instantiate(Prefab, GridParent);
        }
        if (level == 5)
            SendScores();
    }
    public void GoNext()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    async Task SendScores()
    {
        var difficulty = PlayerPrefs.GetInt(SettingsManager.DIFFICULTY_KEY, 1);
        var res = await APIManager.Instance.SendGameData(null, (error) =>
        {
            //PopupController.Instance.ShowPopup("Connection Error","Error","Ok");
        }
        , difficulty-1
        , PlayerPrefs.GetInt("Level2", 0)
        , PlayerPrefs.GetInt("Level3", 0)
        , PlayerPrefs.GetInt("Level4", 0)
        , PlayerPrefs.GetInt("Level5", 0));
    }
}
