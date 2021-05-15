using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tomfulghum.EventSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] BasicEvent gameStartEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnStartButton()
    {
        gameStartEvent.Raise();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
