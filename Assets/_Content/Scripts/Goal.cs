using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tomfulghum.EventSystem;

public class Goal : MonoBehaviour
{
    [SerializeField] IntEvent levelFinishedEvent;
    [SerializeField] LevelData levelData;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float levelFinishDelay = 0.5f;

    bool locked = false;

    void Update()
    {
        if (!locked && Physics2D.OverlapCircle(transform.position, 0.1f, playerMask)) {
            locked = true;
            StartCoroutine(LevelFinishedCoroutine());
        }
    }

    IEnumerator LevelFinishedCoroutine()
    {
        yield return new WaitForSeconds(levelFinishDelay);
        levelFinishedEvent.Raise(levelData.Id);
    }
}
