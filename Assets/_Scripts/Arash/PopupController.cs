using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public static PopupController Instance { get; private set; }

    [SerializeField] private RTLTextMeshPro titleText;
    [SerializeField] private RTLTextMeshPro messageText;
    [SerializeField] private RTLTextMeshPro buttonText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        closeButton.onClick.AddListener(HidePopup);
        HidePopup();
    }
    public void ShowPopup(string message,string title,string buttonTitle)
    {
        titleText.text = title;
        messageText.text = message;
        buttonText.text = buttonTitle;
        gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}