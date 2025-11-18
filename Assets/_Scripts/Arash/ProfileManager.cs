using RTLTMPro;
using UnityEngine;

public class ProfileManager : SingletonBehaviour<ProfileManager>
{
    [SerializeField] public RTLTextMeshPro classRank;
    [SerializeField] public RTLTextMeshPro globalRank;
    [SerializeField] public RTLTextMeshPro points;
    [SerializeField] public RTLTextMeshPro difficultyPointsText;
    [SerializeField] public RTLTextMeshPro nameText;
    [SerializeField] public StatObject easyStat;
    [SerializeField] public StatObject mediumStat;
    [SerializeField] public StatObject hardStat;
    [SerializeField] public RectTransform level1Rect;
    [SerializeField] public RectTransform level2Rect;
    [SerializeField] public RectTransform level3Rect;
    [SerializeField] public RectTransform level4Rect;
    [SerializeField] RectTransform recordRectTransform;
    private void Start()
    {
        nameText.text=PlayerPrefs.GetString("username", "username");
    }
    public void SetSize(int level,int percent)
    {
        float maxHeight = recordRectTransform.sizeDelta.y;
        switch (level) {
            case 1:
                level1Rect.sizeDelta = new Vector2(level1Rect.sizeDelta.x, maxHeight * percent / 100);
                break;
            case 2:
                level2Rect.sizeDelta = new Vector2(level2Rect.sizeDelta.x, maxHeight * percent / 100);
                break;
            case 3:
                level3Rect.sizeDelta = new Vector2(level3Rect.sizeDelta.x, maxHeight * percent / 100);
                break;
            case 4:
                level4Rect.sizeDelta = new Vector2(level4Rect.sizeDelta.x, maxHeight * percent / 100);
                break;
            default:
                break;
        }

    }


}
