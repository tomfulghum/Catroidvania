using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using tomfulghum.EventSystem;

public class GameStateManager : SingletonBehaviour<GameStateManager>
{
    [SerializeField] BasicEvent gameStartedEvent;
    [SerializeField] IntEvent levelFinishedEvent;
    [SerializeField] IntEvent stepCountChangedEvent;

    CatType leadingCatType;
    int currentLevelId;
    int stepCounter;

    public CatType LeadingCatType => leadingCatType;

    void OnEnable()
    {
        CongaCat.OnLeaderCatTypeChanged += OnLeaderCatTypeChanged;
        CongaCat.OnStep += OnStep;
    }

    void OnDisable()
    {
        CongaCat.OnLeaderCatTypeChanged -= OnLeaderCatTypeChanged;
        CongaCat.OnStep -= OnStep;
    }

    void Start()
    {
        currentLevelId = 0;
        stepCounter = 0;
    }

    void OnLeaderCatTypeChanged(CatType type)
    {
        leadingCatType = type;
    }

    void OnStep()
    {
        stepCounter++;
        stepCountChangedEvent.Raise(stepCounter);
    }

    IEnumerator LevelResetCoroutine()
    {
        var loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!loadOperation.isDone)
            yield return null;

        levelFinishedEvent.Raise(currentLevelId - 1);
    }

    public void OnLevelReset()
    {
        StartCoroutine(LevelResetCoroutine());
        stepCounter = 0;
        stepCountChangedEvent.Raise(stepCounter);
    }

    public void OnLevelFinished(int id)
    {
        currentLevelId = id + 1;
        stepCounter = 0;
        stepCountChangedEvent.Raise(stepCounter);
    }
}
