using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using tomfulghum.EventSystem;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] BasicEvent gameStartedEvent;
    [SerializeField] IntEvent levelFinishedEvent;
    [SerializeField] IntEvent stepCountChangedEvent;

    static GameStateManager instance;

    static CatType leadingCatType;

    int currentLevelId;
    int stepCounter;

    public static CatType LeadingCatType => leadingCatType;

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

    void Awake()
    {
        if (instance) {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }

        DontDestroyOnLoad(this);
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
