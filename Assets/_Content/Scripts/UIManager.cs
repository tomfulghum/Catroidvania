using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using tomfulghum.EventSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] BasicEvent levelResetEvent;
    [SerializeField] BasicEvent gameExitEvent;
    [SerializeField] TextMeshProUGUI stepCounter;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnResetButton()
    {
        levelResetEvent.Raise();
    }

    public void OnExitButton()
    {
        gameExitEvent.Raise();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnStepCountChanged(int stepCount)
    {
        stepCounter.text = stepCount.ToString();
    }
}
