using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using tomfulghum.EventSystem;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] BasicEvent gameStartedEvent;
    [SerializeField] IntEvent levelFinishedEvent;

    static GameStateManager instance;

    static CatType leadingCatType;

    int currentLevelId;

    public static CatType LeadingCatType => leadingCatType;

    void OnEnable()
    {
        CongaCat.OnLeaderCatTypeChanged += OnLeaderCatTypeChanged;
    }

    void OnDisable()
    {
        CongaCat.OnLeaderCatTypeChanged -= OnLeaderCatTypeChanged;
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
    }

    void OnLeaderCatTypeChanged(CatType type)
    {
        leadingCatType = type;
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
    }

    public void OnLevelFinished(int id)
    {
        currentLevelId = id + 1;
    }
}
