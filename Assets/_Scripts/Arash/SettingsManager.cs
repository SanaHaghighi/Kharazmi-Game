using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : SingletonBehaviour<SettingsManager>
{
    [Header("UI References")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private ToggleGroup difficultyToggleGroup;
    [SerializeField] private Toggle easyToggle;
    [SerializeField] private Toggle mediumToggle;
    [SerializeField] private Toggle hardToggle;

    [Header("Settings")]
    [SerializeField] private float defaultVolume = 0.5f;
    [SerializeField] private bool defaultMute = false;
    [SerializeField] private int defaultDifficulty = 1; 

    private const string VOLUME_KEY = "Volume";
    private const string MUTE_KEY = "Mute";
    public static string DIFFICULTY_KEY = "Difficulty";

    private void Start()
    {
        LoadSettings();

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteChanged);
        easyToggle.onValueChanged.AddListener((isOn) => OnDifficultyChanged(1, isOn));
        mediumToggle.onValueChanged.AddListener((isOn) => OnDifficultyChanged(2, isOn));
        hardToggle.onValueChanged.AddListener((isOn) => OnDifficultyChanged(3, isOn));
    }

    public void LoadSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, defaultVolume);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        bool savedMute = PlayerPrefs.GetInt(MUTE_KEY, defaultMute ? 1 : 0) == 1;
        muteToggle.isOn = savedMute;
        AudioListener.pause = savedMute;

        int savedDifficulty = PlayerPrefs.GetInt(DIFFICULTY_KEY, defaultDifficulty);
        SetDifficultyToggle(savedDifficulty);
    }

    private void OnVolumeChanged(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
    }

    private void OnMuteChanged(bool isMuted)
    {
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt(MUTE_KEY, isMuted ? 1 : 0);
    }

    private void OnDifficultyChanged(int difficulty, bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
            Debug.Log($"Difficulty set to: {difficulty}");
        }
    }

    private void SetDifficultyToggle(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                easyToggle.isOn = true;
                break;
            case 2:
                mediumToggle.isOn = true;
                break;
            case 3:
                hardToggle.isOn = true;
                break;
            default:
                easyToggle.isOn = true;
                break;
        }
    }

    public void ResetToDefaults()
    {
        volumeSlider.value = defaultVolume;
        OnVolumeChanged(defaultVolume);

        muteToggle.isOn = defaultMute;
        OnMuteChanged(defaultMute);

        SetDifficultyToggle(defaultDifficulty);
        PlayerPrefs.SetInt(DIFFICULTY_KEY, defaultDifficulty);
    }
}