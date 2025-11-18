using RTLTMPro;
using UnityEngine;

public class ScaleWeight : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro text;
    public void ShowText(string txt)
    {
        text.text=txt;
    }
}
