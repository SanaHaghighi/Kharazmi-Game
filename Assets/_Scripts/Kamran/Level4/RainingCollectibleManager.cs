using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RainingCollectibleManager : SingletonBehaviour<RainingCollectibleManager>
{
    [SerializeField] RainingCollectible prefab;
    [SerializeField] Transform parent;
    [SerializeField] float spawnPaddingFromTop;
    [SerializeField] float spawnPaddingFromBot;
    [SerializeField] float spawnPaddingFromEdge;
    [SerializeField] float spawnPaddingFromOthers;
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;
    List<RainingCollectible> spawnedXs;
    bool startRaining;
    private float minX;
    private float maxX;
    private float maxY;
    private float minY;
    public bool StartRaining
    {
        get { 
            return startRaining; 
        } 
        set { 
            startRaining = value; 
        }
    }
    public void Init()
    {
        startRaining = true;
        spawnedXs = new ();
        StartCoroutine(Rain());
    }
    private IEnumerator Rain()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        Vector3 camPosition = cam.transform.position;
        minX = camPosition.x - camWidth / 2 + spawnPaddingFromEdge;
        maxX = camPosition.x + camWidth / 2 - spawnPaddingFromEdge;
        maxY = camPosition.y + camHeight / 2 + spawnPaddingFromTop;
        minY = camPosition.y - camHeight / 2 + spawnPaddingFromBot;
        int cnt = 0;
        while (StartRaining)
        {
            var pos=new Vector2(Random.Range(minX, maxX), maxY + Random.Range(0,2f));

            if (!CheckForSafetey(pos) && cnt<100)
            {
                cnt++;
                continue;
            }

            var newRain=Instantiate(prefab, parent);
            newRain.transform.position = pos;
            newRain.Init(minY);
            spawnedXs.Add(newRain);
            cnt = 0;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    private bool CheckForSafetey(Vector2 pos)
    {
        foreach(var i in spawnedXs)
        {
            if (Mathf.Abs(i.transform.position.x - pos.x) < spawnPaddingFromOthers)
            {
                if(i.transform.position.y>maxY-1)
                    return false;
            }
        }
        return true;
    }

    public void RemoveRain(RainingCollectible rainingCollectible)
    {
        spawnedXs.Remove(rainingCollectible);
        Destroy(rainingCollectible.gameObject);
    }
    public void ClickedRain(RainingCollectible rainingCollectible)
    {
        Level4Manager.Instance.ClickedOnRain(rainingCollectible);
        RemoveRain(rainingCollectible);
    }
}
