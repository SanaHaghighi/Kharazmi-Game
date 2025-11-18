using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToSnake()
    {
        SceneManager.LoadScene("Level 2");
    }
}
