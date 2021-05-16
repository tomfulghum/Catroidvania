using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tomfulghum.EventSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] BasicEvent resetGame;
    [SerializeField] BasicEvent goToMainMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnResetButton()
    {
        resetGame.Raise();
    }

    public void OnExitButton()
    {
        goToMainMenu.Raise();
    }

    public void StepCounter(int stepCount)
    {

    }
}
