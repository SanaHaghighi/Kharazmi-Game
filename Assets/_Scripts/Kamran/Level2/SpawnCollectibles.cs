using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : SingletonBehaviour<SpawnCollectibles>
{
    public CollectibleManager prefab;
    public int NumberOfDesiredKeys;
    public int InitialNumberOfObjects = 10;
    public int MaxNumberOfObjects = 20;
    [SerializeField] float spawnPaddingFromCamera;
    [SerializeField] float spawnPaddingFromCollectible;
    [SerializeField] float spawnPaddingFromHeader;
    [SerializeField] Transform collectibleParent;
    public List<CollectibleManager> AllCollectibles;
    private List<int> selectedNumbers;
    int numberOfCollectibles;

    public void SpawnCollectible()
    {
        numberOfCollectibles = 0;
        AllCollectibles = new();
        selectedNumbers = Level2Manager.GetUniqueRandomNumbers(NumberOfDesiredKeys,InitialNumberOfObjects-1);
        Debug.Log(string.Join(", ", selectedNumbers));
        SpawnObjects(InitialNumberOfObjects);
    }

    void SpawnObjects(int numberOfObjects=1)
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        Vector3 camPosition = cam.transform.position;
        float minX = camPosition.x - camWidth / 2 + spawnPaddingFromCamera;
        float maxX = camPosition.x + camWidth / 2 - spawnPaddingFromCamera;
        float minY = camPosition.y - camHeight / 2 + spawnPaddingFromCamera;
        float maxY = camPosition.y + camHeight / 2 - spawnPaddingFromCamera - spawnPaddingFromHeader;
        var desiredKeyString = PlayerManager.Instance.KeyString;
        int cnt = 0;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                0f
            );
            if (!CheckForSafety(spawnPosition) && cnt<10000)
            {
                i--;
                cnt++;
                continue;
            }
            if (cnt >= 10000)
            {
                Debug.LogError("Problemn");
            }
            var selectedKey = "nothing";
            if (selectedNumbers.Contains(numberOfCollectibles))
            {
                selectedKey = desiredKeyString;
            }

            var newCollectible=Instantiate(prefab, collectibleParent,collectibleParent);
            newCollectible.transform.position = spawnPosition;
            newCollectible.Init(selectedKey);
            AllCollectibles.Add(newCollectible);
            numberOfCollectibles++;
            newCollectible.transform.DOKill(true);
            newCollectible.transform.DOPunchScale(newCollectible.transform.localScale * .2f, 1f);

            cnt = 0;
        }
        Debug.Log("Spawned");
    }
    bool CheckForSafety(Vector3 spawnPosition)
    {
        bool isSafe = true;
        foreach (var collectible in AllCollectibles)
        {
            if (Vector3.Distance(collectible.transform.position, spawnPosition) < spawnPaddingFromCollectible)
            {
                isSafe = false; break;
            }
        }
        foreach(var seg in PlayerManager.Instance.segments)
        {
            if (Vector3.Distance(seg.transform.position, spawnPosition) < spawnPaddingFromCollectible)
            {
                isSafe = false;break;
            }
        }
        return isSafe;
    }
    public void CollectibleEaten(CollectibleManager collectible)
    {
        SpawnObjects(1);
        AllCollectibles.Remove(collectible);
    }
}
