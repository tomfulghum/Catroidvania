using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tomfulghum.EventSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] BasicEvent gameStartedEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnStartButton()
    {
        gameStartedEvent.Raise();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
