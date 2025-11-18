using System;
using SFXSystem;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect; // Assign the Scroll View's ScrollRect
    [SerializeField] private GameObject nextButton; // Assign the Button that will be activated

    [SerializeField] private GameObject[] g;


    private void Awake()
    {
        SoundSystemManager.Instance.Setup();
    }

    private void Update()
    {
        // Check if the scroll position is at the end
        if (scrollRect.verticalNormalizedPosition <= 0.01f) // Adjust the threshold as needed
        {
            nextButton.gameObject.SetActive(true); // Activate the button
        }
        else
        {
            nextButton.gameObject.SetActive(false); // Deactivate the button
        }
    }

    public void Deactivater()
    {
        g[0].SetActive(false);
        g[1].SetActive(true);
        
    }

    
}
