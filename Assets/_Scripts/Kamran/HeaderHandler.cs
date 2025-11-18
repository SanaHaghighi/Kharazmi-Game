using UnityEngine;
using UnityEngine.UI;

public class HeaderHandler : MonoBehaviour
{
    [SerializeField] Button MapBtn;
    [SerializeField] Button infoBtn;
    [SerializeField] GameObject timer;
    [SerializeField] int level;
    private void Start()
    {
        infoBtn.onClick.AddListener(()=>TutorialManager.Instance.OpenTutorial());
        MapBtn.onClick.AddListener(() => RoadMapManager.Instance.OpenRoadMap(level));
    }
    public void SetTimeObjectEnable(bool val)
    {
        timer.SetActive(val);
    }
}
