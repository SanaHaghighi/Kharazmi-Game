using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ClickedThingsManager : MonoBehaviour
{
    [SerializeField] ClickedThing prefab;
    [SerializeField] ScrollRect scrollbar;
    [SerializeField] Transform parent;
    public void Init()
    {
        ResetRains();
        Level4Manager.Instance.ClickedRain += AddRainCaller;
        Level4Manager.Instance.FinishedEquation += ResetRains;
    }

    private void ResetRains()
    {
        foreach (Transform t in parent)
        {
            Destroy(t.gameObject);
        }
    }
    void AddRainCaller(RainingCollectible collectible, int value, bool isKey)
    {
        StartCoroutine(AddRain(collectible,value,isKey));
    }
    private IEnumerator AddRain(RainingCollectible collectible,int value,bool isKey)
    {
        var thing = Instantiate(prefab, parent);
        thing.Repaint(collectible.KeyText,value,isKey);
        yield return new WaitForSeconds(.1f);
        scrollbar.verticalNormalizedPosition = 0f;
    }
}
