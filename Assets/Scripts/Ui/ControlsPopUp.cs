using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPopUp : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> controlPopUps;
    [SerializeField]
    private List<GameObject> UiElements;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnControlsClicked()
    {
        foreach (GameObject uiElement in UiElements)
        {
            uiElement.SetActive(!uiElement.activeSelf);
        }
        foreach (GameObject popUp in controlPopUps)
        {
            popUp.SetActive(!popUp.activeSelf);
        }
    }
}
